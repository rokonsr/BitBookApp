using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitBookApp.Models
{
    public class ProfileImage
    {
        [Key]
        public int ProfileImageId { get; set; }

        public int UserId { get; set; }

        [Required(ErrorMessage = "Image type is required")]
        [Display(Name = "Image Type")]
        public int ImageTypeId { get; set; }

        [Required(ErrorMessage = "Image is required")]
        [Display(Name = "Profile Photo")]
        public byte[] UserImage { get; set; }
    }
}