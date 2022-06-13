namespace Candor.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdeaId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Idea", "IdeaId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Idea", "IdeaId");
        }
    }
}
