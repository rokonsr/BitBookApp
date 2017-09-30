using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitBookApp.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string UserComment { get; set; }
    }
}