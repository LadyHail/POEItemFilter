namespace POEItemFilter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEntityTypeConfigToItemTypeTable : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ItemTypes");
            AlterColumn("dbo.ItemTypes", "Id", c => c.Byte(nullable: false, identity: true));
            AlterColumn("dbo.ItemTypes", "Name", c => c.String(nullable: false, maxLength: 50));
            AddPrimaryKey("dbo.ItemTypes", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ItemTypes");
            AlterColumn("dbo.ItemTypes", "Name", c => c.String());
            AlterColumn("dbo.ItemTypes", "Id", c => c.Byte(nullable: false));
            AddPrimaryKey("dbo.ItemTypes", "Id");
        }
    }
}
