namespace Candor.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Idea", "UserId", c => c.Guid(nullable: false));
            AddColumn("dbo.Rating", "UserId", c => c.Guid(nullable: false));
            DropColumn("dbo.Idea", "OwnerId");
            DropColumn("dbo.Rating", "OwnerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rating", "OwnerId", c => c.Guid(nullable: false));
            AddColumn("dbo.Idea", "OwnerId", c => c.Guid(nullable: false));
            DropColumn("dbo.Rating", "UserId");
            DropColumn("dbo.Idea", "UserId");
        }
    }
}
