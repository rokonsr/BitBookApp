using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BitBookApp.Models
{
    public class UserRegistration
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Please input valid email address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}