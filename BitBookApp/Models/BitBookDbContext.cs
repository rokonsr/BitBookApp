using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BitBookApp.Models
{
    public class BitBookDbContext : DbContext
    {
        public DbSet<UserRegistration> Registrations { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<ProfileImage> ProfileImages { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}