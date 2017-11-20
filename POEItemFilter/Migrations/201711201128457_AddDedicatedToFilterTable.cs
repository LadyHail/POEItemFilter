namespace POEItemFilter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDedicatedToFilterTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Filters", "Dedicated", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Filters", "Dedicated");
        }
    }
}
