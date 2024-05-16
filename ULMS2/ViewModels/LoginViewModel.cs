using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ULMS2.ViewModels
{
    public class LoginViewModel
    {
        [DisplayName("Email address")]
        [Display(Prompt = "Enter your email address")]
        [Required(ErrorMessage = "Email address is required"), DataType(DataType.EmailAddress), EmailAddress]
        [MaxLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string Email { get; set; }

        [DisplayName("Password")]
        [Display(Prompt = "Enter your password")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Remember me")]
        public bool RememberMe { get; set; }
    }
}
