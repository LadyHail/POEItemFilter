namespace POEItemFilter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelUpdate : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ItemDBs", newName: "ItemsDB");
            CreateTable(
                "dbo.ItemBaseTypes",
                c => new
                    {
                        Id = c.Byte(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ItemBaseTypeId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ItemBaseTypes", t => t.ItemBaseTypeId)
                .Index(t => t.ItemBaseTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemTypes", "ItemBaseTypeId", "dbo.ItemBaseTypes");
            DropIndex("dbo.ItemTypes", new[] { "ItemBaseTypeId" });
            DropTable("dbo.ItemTypes");
            DropTable("dbo.ItemBaseTypes");
            RenameTable(name: "dbo.ItemsDB", newName: "ItemDBs");
        }
    }
}
