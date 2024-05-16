using ULMS2.Models;

namespace ULMS2.ViewModels
{
    public class BookIndexViewModel
    {
        public List<Book> Books { get; set; }

        public List<int> UserBorrowedBooks { get; set; }
    }
}
