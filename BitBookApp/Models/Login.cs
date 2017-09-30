using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitBookApp.Models
{
    public class Login
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Please input valid email address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}