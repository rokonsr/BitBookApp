using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitBookApp.Models
{
    public class ViewFriendRequiest
    {
        public int FriendId { get; set; }
        public string FriendName { get; set; }
        public byte[] UserImage { get; set; }
    }
}