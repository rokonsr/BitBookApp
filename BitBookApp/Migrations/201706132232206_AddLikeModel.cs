namespace BitBookApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLikeModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        LikeId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        PostId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LikeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Likes");
        }
    }
}
