namespace POEItemFilter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConnectUsersItemsWithFilterTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ItemUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FilterId = c.Int(nullable: false),
                        MainCategory = c.String(),
                        Class = c.String(),
                        Attribute1 = c.String(),
                        Attribute2 = c.String(),
                        BaseType = c.String(),
                        DropLevel = c.String(),
                        Rarity = c.String(),
                        Quality = c.String(),
                        ItemLevel = c.String(),
                        Sockets = c.String(),
                        SocketsGroup = c.String(),
                        LinkedSockets = c.String(),
                        Height = c.String(),
                        Width = c.String(),
                        SetBorderColor = c.String(),
                        SetTextColor = c.String(),
                        SetBackgroundColor = c.String(),
                        SetFontSize = c.String(nullable: false),
                        Identified = c.Boolean(nullable: false),
                        Corrupted = c.Boolean(nullable: false),
                        Show = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Filters", t => t.FilterId)
                .Index(t => t.FilterId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemUsers", "FilterId", "dbo.Filters");
            DropIndex("dbo.ItemUsers", new[] { "FilterId" });
            DropTable("dbo.ItemUsers");
        }
    }
}
