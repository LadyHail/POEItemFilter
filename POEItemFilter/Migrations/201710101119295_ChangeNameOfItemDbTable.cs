namespace POEItemFilter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeNameOfItemDbTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ItemDBs", newName: "ItemsDB");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ItemsDB", newName: "ItemDBs");
        }
    }
}
