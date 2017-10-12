namespace POEItemFilter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinalModelForItemsFromWeb : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            RenameColumn(table: "dbo.ItemTypes", name: "ItemBaseTypeId", newName: "BaseTypeId");
            RenameIndex(table: "dbo.ItemTypes", name: "IX_ItemBaseTypeId", newName: "IX_BaseTypeId");
            CreateTable(
                "dbo.ItemAttributes",
                c => new
                    {
                        Id = c.Byte(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        BaseTypeId = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ItemBaseTypes", t => t.BaseTypeId)
                .Index(t => t.BaseTypeId);
            
            CreateTable(
                "dbo.AttributesPerTypes",
                c => new
                    {
                        TypeId = c.Int(nullable: false),
                        AttributeId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => new { t.TypeId, t.AttributeId })
                .ForeignKey("dbo.ItemTypes", t => t.TypeId)
                .ForeignKey("dbo.ItemAttributes", t => t.AttributeId)
                .Index(t => t.TypeId)
                .Index(t => t.AttributeId);
            
            CreateTable(
                "dbo.AttributesPerItems",
                c => new
                    {
                        AttributeId = c.Byte(nullable: false),
                        ItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AttributeId, t.ItemId })
                .ForeignKey("dbo.ItemAttributes", t => t.AttributeId)
                .ForeignKey("dbo.ItemsDB", t => t.ItemId)
                .Index(t => t.AttributeId)
                .Index(t => t.ItemId);
            
            AddColumn("dbo.ItemsDB", "BaseTypeId", c => c.Byte(nullable: false));
            AddColumn("dbo.ItemsDB", "TypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.ItemsDB", "BaseTypeId");
            CreateIndex("dbo.ItemsDB", "TypeId");
            AddForeignKey("dbo.ItemsDB", "BaseTypeId", "dbo.ItemBaseTypes", "Id");
            AddForeignKey("dbo.ItemsDB", "TypeId", "dbo.ItemTypes", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id");
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.ItemsDB", "Type");
            DropColumn("dbo.ItemsDB", "Attribute1");
            DropColumn("dbo.ItemsDB", "Attribute2");
            DropColumn("dbo.ItemsDB", "BaseType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ItemsDB", "BaseType", c => c.Int(nullable: false));
            AddColumn("dbo.ItemsDB", "Attribute2", c => c.Int());
            AddColumn("dbo.ItemsDB", "Attribute1", c => c.Int());
            AddColumn("dbo.ItemsDB", "Type", c => c.Int(nullable: false));
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AttributesPerItems", "ItemId", "dbo.ItemsDB");
            DropForeignKey("dbo.AttributesPerItems", "AttributeId", "dbo.ItemAttributes");
            DropForeignKey("dbo.ItemsDB", "TypeId", "dbo.ItemTypes");
            DropForeignKey("dbo.AttributesPerTypes", "AttributeId", "dbo.ItemAttributes");
            DropForeignKey("dbo.AttributesPerTypes", "TypeId", "dbo.ItemTypes");
            DropForeignKey("dbo.ItemsDB", "BaseTypeId", "dbo.ItemBaseTypes");
            DropForeignKey("dbo.ItemAttributes", "BaseTypeId", "dbo.ItemBaseTypes");
            DropIndex("dbo.AttributesPerItems", new[] { "ItemId" });
            DropIndex("dbo.AttributesPerItems", new[] { "AttributeId" });
            DropIndex("dbo.AttributesPerTypes", new[] { "AttributeId" });
            DropIndex("dbo.AttributesPerTypes", new[] { "TypeId" });
            DropIndex("dbo.ItemsDB", new[] { "TypeId" });
            DropIndex("dbo.ItemsDB", new[] { "BaseTypeId" });
            DropIndex("dbo.ItemAttributes", new[] { "BaseTypeId" });
            DropColumn("dbo.ItemsDB", "TypeId");
            DropColumn("dbo.ItemsDB", "BaseTypeId");
            DropTable("dbo.AttributesPerItems");
            DropTable("dbo.AttributesPerTypes");
            DropTable("dbo.ItemAttributes");
            RenameIndex(table: "dbo.ItemTypes", name: "IX_BaseTypeId", newName: "IX_ItemBaseTypeId");
            RenameColumn(table: "dbo.ItemTypes", name: "BaseTypeId", newName: "ItemBaseTypeId");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
        }
    }
}
