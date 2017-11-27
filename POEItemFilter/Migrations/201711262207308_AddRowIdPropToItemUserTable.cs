namespace POEItemFilter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRowIdPropToItemUserTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemUsers", "RowId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemUsers", "RowId");
        }
    }
}
