using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ULMS2.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public string StudentId { get; set; }
        public Student Student { get; set; }

        public string FacultyId { get; set; }
        public Faculty Faculty { get; set; }

        public string LibrarianId { get; set; }
        public Librarian Librarian { get; set; }

        [Required]
        public DateTime DateBorrowed { get; set; }

        public DateTime? DateReturned { get; set; }

        [NotMapped]
        public DateTime DateDue { get { return DateBorrowed.AddDays(14); } }

        //[NotMapped]
        //public bool IsReturned { get { return DateReturned == null; } }
    }
}
