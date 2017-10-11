namespace POEItemFilter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBaseTypesModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BaseTypes",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BaseTypes");
        }
    }
}
