using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ULMS2.Models
{
    public class Librarian : Account
    {
        [Required]
        [MaxLength(75)]
        [Column(TypeName = "varchar(75)")]
        public string Name { get; set; }
        
        public ICollection<Transaction> Transactions { get; set; }
    }
}
