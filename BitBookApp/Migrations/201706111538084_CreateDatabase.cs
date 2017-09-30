namespace BitBookApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserRegistrations",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        EmailAddress = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        ProfileId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Gender = c.String(),
                        Contact = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        AreaOfInterest = c.String(),
                    })
                .PrimaryKey(t => t.ProfileId);
            
            CreateTable(
                "dbo.Educations",
                c => new
                    {
                        EducationId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        EducationTitle = c.String(nullable: false),
                        Institute = c.String(nullable: false),
                        PassingYear = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EducationId);
            
            CreateTable(
                "dbo.Experiences",
                c => new
                    {
                        ExperienceId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ExpDesignation = c.String(nullable: false),
                        ExpCompany = c.String(nullable: false),
                        ExpYear = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ExperienceId);
            
            CreateTable(
                "dbo.ProfileImages",
                c => new
                    {
                        ProfileImageId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ImageTypeId = c.Int(nullable: false),
                        UserImage = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.ProfileImageId);
            
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        FriendshipId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FriendId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FriendshipId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Friends");
            DropTable("dbo.ProfileImages");
            DropTable("dbo.Experiences");
            DropTable("dbo.Educations");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.UserRegistrations");
        }
    }
}
