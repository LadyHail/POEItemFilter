namespace POEItemFilter.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddFluentApiToBaseTypeAndType : DbMigration
    {
        public override void Up()
        {
            Sql(@"ALTER TABLE [dbo].[ItemTypes] DROP CONSTRAINT [FK_dbo.Types_dbo.BaseTypes_BaseType_Id]");
            DropForeignKey("dbo.ItemTypes", "ItemBaseType_Id", "dbo.ItemBaseTypes");
            DropPrimaryKey("dbo.ItemBaseTypes");
            AlterColumn("dbo.ItemBaseTypes", "Id", c => c.Byte(nullable: false, identity: true));
            AlterColumn("dbo.ItemBaseTypes", "Name", c => c.String(nullable: false, maxLength: 50));
            AddPrimaryKey("dbo.ItemBaseTypes", "Id");
            AddForeignKey("dbo.ItemTypes", "ItemBaseType_Id", "dbo.ItemBaseTypes", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.ItemTypes", "ItemBaseType_Id", "dbo.ItemBaseTypes");
            DropPrimaryKey("dbo.ItemBaseTypes");
            AlterColumn("dbo.ItemBaseTypes", "Name", c => c.String());
            AlterColumn("dbo.ItemBaseTypes", "Id", c => c.Byte(nullable: false));
            AddPrimaryKey("dbo.ItemBaseTypes", "Id");
            AddForeignKey("dbo.ItemTypes", "ItemBaseType_Id", "dbo.ItemBaseTypes", "Id");
        }
    }
}
