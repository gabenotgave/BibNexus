using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ULMS2.Models
{
    public class Faculty : Account
    {
        [Required]
        [MaxLength(75)]
        [Column(TypeName = "varchar(75)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(24)")]
        public string Department { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
