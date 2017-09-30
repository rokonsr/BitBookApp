using System.ComponentModel.DataAnnotations;

namespace BitBookApp.Models
{
    public class UserProfile
    {
        [Key]
        public int ProfileId { get; set; }

        public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Contact { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        [Display(Name = "Area Of Interest")]
        public string AreaOfInterest { get; set; }
    }
}