using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ULMS2.Models
{
    public class Book
    {
        public int Id { get; set; }

        [DisplayName("Title")]
        [Display(Prompt = "The Handmaid's Tale")]
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Title { get; set; }

        [DisplayName("Authors")]
        [Display(Prompt = "Margaret Atwood")]
        [Required(ErrorMessage = "Authors are required")]
        [MaxLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string Authors { get; set; }

        [DisplayName("ISBN")]
        [Display(Prompt = "9780385539241")]
        [Required(ErrorMessage = "ISBN is required")]
        [MaxLength(13)]
        [Column(TypeName = "varchar(13)")]
        public string ISBN { get; set; }

        [DisplayName("Date published")]
        [Required(ErrorMessage = "Date published is required")]
        public DateTime DatePublished { get; set; }

        [DisplayName("Category")]
        [Display(Prompt = "Dystopian")]
        [Required(ErrorMessage = "Category is required")]
        [MaxLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string Category { get; set; }

        [DisplayName("Is available")]
        public bool IsAvailable { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        [NotMapped]
        public Transaction MostRecentTransaction { get { return Transactions?.OrderByDescending(t => t.DateBorrowed).FirstOrDefault(); } set { } }
    }
}
