using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ULMS2.Data;
using ULMS2.Models;

namespace ULMS2.Controllers
{
    [Authorize(Roles = "Student,Faculty")]
    public class HistoryController : Controller
    {
        public readonly ApplicationDbContext context;

        public HistoryController(ApplicationDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string accountId = User.Identity.Name;
            List<Transaction> transactions = await context.Transactions
                .Where(t =>
                    t.StudentId == accountId ||
                    t.FacultyId == accountId ||
                    t.LibrarianId == accountId)
                .Include(t => t.Book)
                .ToListAsync();
            return View(transactions);
        }
    }
}
