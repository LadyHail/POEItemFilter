namespace POEItemFilter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnableCascadeDeleteForItemsAndFilters : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ItemUsers", "FilterId", "dbo.Filters");
            AddForeignKey("dbo.ItemUsers", "FilterId", "dbo.Filters", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemUsers", "FilterId", "dbo.Filters");
            AddForeignKey("dbo.ItemUsers", "FilterId", "dbo.Filters", "Id");
        }
    }
}
