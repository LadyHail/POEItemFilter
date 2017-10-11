namespace POEItemFilter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveBaseTypePropFromItemTypeTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ItemTypes", name: "BaseType_Id", newName: "ItemBaseType_Id");
            RenameIndex(table: "dbo.ItemTypes", name: "IX_BaseType_Id", newName: "IX_ItemBaseType_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ItemTypes", name: "IX_ItemBaseType_Id", newName: "IX_BaseType_Id");
            RenameColumn(table: "dbo.ItemTypes", name: "ItemBaseType_Id", newName: "BaseType_Id");
        }
    }
}
