using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitBookApp.Models
{
    public class Experience
    {
        [Key]
        public int ExperienceId { get; set; }

        public int UserId { get; set; }

        [Required(ErrorMessage = "Please provide Designation")]
        [Display(Name = "Designation")]
        public string ExpDesignation { get; set; }

        [Required(ErrorMessage = "Please provide Organization")]
        [Display(Name = "Company")]
        public string ExpCompany { get; set; }

        [Required(ErrorMessage = "Please provide Experience")]
        [Display(Name = "Year of Experience")]
        public string ExpYear { get; set; }
    }
}