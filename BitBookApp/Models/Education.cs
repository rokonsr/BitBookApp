using System.ComponentModel.DataAnnotations;

namespace BitBookApp.Models
{
    public class Education
    {
        [Key]
        public int EducationId { get; set; }

        public int UserId { get; set; }

        [Required(ErrorMessage = "Please provide Educatoin Title")]
        [Display(Name = "Title")]
        public string EducationTitle { get; set; }

        [Required(ErrorMessage = "Please provide Institute")]
        public string Institute { get; set; }

        [Required(ErrorMessage = "Please provide Passing Year")]
        [Display(Name = "Passing Year")]
        public string PassingYear { get; set; }
    }
}