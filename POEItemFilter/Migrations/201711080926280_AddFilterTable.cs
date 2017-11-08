namespace POEItemFilter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFilterTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Filters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 500),
                        CreateDate = c.DateTime(nullable: false),
                        EditDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Filters");
        }
    }
}
