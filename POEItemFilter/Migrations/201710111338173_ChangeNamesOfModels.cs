namespace POEItemFilter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeNamesOfModels : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.BaseTypes", newName: "ItemBaseTypes");
            RenameTable(name: "dbo.Types", newName: "ItemTypes");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ItemTypes", newName: "Types");
            RenameTable(name: "dbo.ItemBaseTypes", newName: "BaseTypes");
        }
    }
}
