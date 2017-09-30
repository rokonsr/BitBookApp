using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitBookApp.Models
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public int Status { get; set; }
    }
}