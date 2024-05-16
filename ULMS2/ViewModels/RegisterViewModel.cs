using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ULMS2.ViewModels
{
    public class RegisterViewModel
    {
        [DisplayName("Name")]
        [Display(Prompt = "Enter your name")]
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(75)]
        [Column(TypeName = "varchar(75)")]
        public string Name { get; set; }

        [DisplayName("Email address")]
        [Display(Prompt = "Enter your email address")]
        [Required(ErrorMessage = "Email address is required"), DataType(DataType.EmailAddress), EmailAddress]
        [MaxLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string Email { get; set; }

        [DisplayName("Password")]
        [Display(Prompt = "Enter your password")]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Confirm password")]
        [Display(Prompt = "Confirm your password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Role")]
        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }

        [DisplayName("Department")]
        [Display(Prompt = "Enter your department")]
        [MaxLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string Department { get; set; }

        public List<string> Roles { get { return new List<string> { "Student", "Faculty", "Librarian" }; } }
    }
}
