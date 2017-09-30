using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitBookApp.Models
{
    public class Friend
    {
        [Key]
        public int FriendshipId { get; set; }
        public int UserId { get; set; }
        public int FriendId { get; set; }
        public bool Status { get; set; }
    }
}