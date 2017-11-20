namespace POEItemFilter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConnectFilterWithUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Filters", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Filters", "UserId");
            AddForeignKey("dbo.Filters", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Filters", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Filters", new[] { "UserId" });
            DropColumn("dbo.Filters", "UserId");
        }
    }
}
