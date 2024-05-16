using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ULMS2.Data;
using ULMS2.Models;
using ULMS2.ViewModels;

namespace ULMS2.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        public readonly ApplicationDbContext context;

        public BooksController(ApplicationDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Retrieve all books from database
            BookIndexViewModel bookIndexVm = new BookIndexViewModel();
            bookIndexVm.Books = await context.Books
                .Include(b => b.Transactions)
                .ToListAsync();
            string userId = User.Identity.Name;
            // Get all book checkouts

            // Get book checkouts associated with client that haven't been returned yet
            bookIndexVm.UserBorrowedBooks = await context.Transactions
                .Where(t =>
                    (t.LibrarianId == userId ||
                    t.StudentId == userId ||
                    t.FacultyId == userId)
                    && t.DateReturned == null)
                .Select(t => t.BookId)
                .ToListAsync();
            return View(bookIndexVm);
        }

        [HttpGet]
        [Authorize(Roles = "Librarian")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Librarian")]
        public async Task<IActionResult> Add(Book book)
        {
            if (!ModelState.IsValid)
                return View(book);

            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();

            TempData["Message"] = "Book successfully added.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Librarian")]
        public async Task<IActionResult> Edit(int id)
        {
            Book book = await context.Books.FirstOrDefaultAsync(b => b.Id == id);
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Librarian")]
        public async Task<IActionResult> Edit(Book book)
        {
            if (!ModelState.IsValid)
                return View(book);

            Book dbBook = await context.Books.Where(b => b.Id == book.Id).Include(b => b.Transactions).AsNoTracking().FirstOrDefaultAsync();

            // If the librarian made the book available mark as returned
            if (dbBook.IsAvailable == false && book.IsAvailable == true)
            {
                await Return(book.Id);
            }

            context.Books.Update(book);
            await context.SaveChangesAsync();

            TempData["Message"] = "Book successfully updated.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Librarian")]
        public async Task<IActionResult> Delete(Book book)
        {
            if (!ModelState.IsValid)
                return View(book);

            context.Books.Remove(book);
            await context.SaveChangesAsync();

            TempData["Message"] = "Book successfully deleted.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<JsonResult> Borrow(int bookId)
        {
            // Ensure book with provided ID exists
            Book book = await context.Books.Where(b => b.Id == bookId).FirstOrDefaultAsync();
            if (book == null)
                return Json(new { success = "false" });

            // Get client role
            string role = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Role).Value;

            // Set stored procedure parameters
            List<SqlParameter> prms = new List<SqlParameter>()
            {
                new SqlParameter("@bookId", bookId),
                new SqlParameter("@accountId", User.Identity.Name)
            };

            // Exeute appropriate stored procedure depending on client's role
            if (role == "Student")
                await context.Database.ExecuteSqlRawAsync($"exec StudentBorrow {String.Join(", ", prms.Select(p => p.ParameterName))}", prms.ToArray());
            else if (role == "Faculty")
                await context.Database.ExecuteSqlRawAsync($"exec FacultyBorrow {String.Join(", ", prms.Select(p => p.ParameterName))}", prms.ToArray());
            else if (role == "Librarian")
                await context.Database.ExecuteSqlRawAsync($"exec LibrarianBorrow {String.Join(", ", prms.Select(p => p.ParameterName))}", prms.ToArray());

            return Json(new { success = "true", due = DateTime.Now.AddDays(14).ToString("MM/dd/yyyy") });
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> Return(int bookId)
        {
            // Ensure book with provided ID exists
            Book book = await context.Books.Where(b => b.Id == bookId).Include(b => b.Transactions).AsNoTracking().FirstOrDefaultAsync();
            if (book == null)
                return NotFound();

            // Set stored procedure parameters
            List<SqlParameter> prms = new List<SqlParameter>()
            {
                new SqlParameter("@bookId", bookId),
                new SqlParameter("@transactionId", book.MostRecentTransaction.Id)
            };

            // Exeute appropriate stored procedure depending on client's role
            await context.Database.ExecuteSqlRawAsync($"exec BookReturn {String.Join(", ", prms.Select(p => p.ParameterName))}", prms.ToArray());

            return Ok();
        }
    }
}
