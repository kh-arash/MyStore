using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace MyStore.Database.Models.Authentication.SignUp
{
    public class RegisterUserModel
    {
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [NotMapped]
        [Compare("Password")]
        [Required(ErrorMessage = "ConfirmPassword is required")]
        public string ConfirmPassword { get; set; }

        public List<string>? Roles { get; set; } = new List<string>();
    }
}
