namespace POEItemFilter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAlertSoundPropToUsersItemsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemUsers", "PlayAlertSound", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemUsers", "PlayAlertSound");
        }
    }
}
