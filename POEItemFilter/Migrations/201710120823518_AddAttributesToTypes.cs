namespace POEItemFilter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttributesToTypes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemBaseTypes", "Attribute1", c => c.Int());
            AddColumn("dbo.ItemBaseTypes", "Attribute2", c => c.Int());
            AddColumn("dbo.ItemTypes", "Attribute1", c => c.Int());
            AddColumn("dbo.ItemTypes", "Attribute2", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemTypes", "Attribute2");
            DropColumn("dbo.ItemTypes", "Attribute1");
            DropColumn("dbo.ItemBaseTypes", "Attribute2");
            DropColumn("dbo.ItemBaseTypes", "Attribute1");
        }
    }
}
