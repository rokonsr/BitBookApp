using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitBookApp.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public int UserId { get; set; }

        [Display(Name = "Text")]
        public string TextPost { get; set; }

        [Display(Name = "Image")]
        public byte[] ImagePost { get; set; }
        public DateTime PostDate { get; set; }
    }
}