namespace POEItemFilter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTypesModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(),
                        BaseType_Id = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BaseTypes", t => t.BaseType_Id)
                .Index(t => t.BaseType_Id);
            
            AlterColumn("dbo.BaseTypes", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Types", "BaseType_Id", "dbo.BaseTypes");
            DropIndex("dbo.Types", new[] { "BaseType_Id" });
            AlterColumn("dbo.BaseTypes", "Name", c => c.Int(nullable: false));
            DropTable("dbo.Types");
        }
    }
}
