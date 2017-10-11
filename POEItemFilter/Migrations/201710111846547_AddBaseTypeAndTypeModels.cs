namespace POEItemFilter.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddBaseTypeAndTypeModels : DbMigration
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
                    Id = c.Byte(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    ItemBaseType_Id = c.Byte(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ItemBaseTypes", t => t.ItemBaseType_Id)
                .Index(t => t.ItemBaseType_Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.ItemTypes", "ItemBaseType_Id", "dbo.ItemBaseTypes");
            DropIndex("dbo.ItemTypes", new[] { "ItemBaseType_Id" });
            DropTable("dbo.ItemTypes");
            DropTable("dbo.ItemBaseTypes");
            RenameTable(name: "dbo.ItemsDB", newName: "ItemDBs");
        }
    }
}
