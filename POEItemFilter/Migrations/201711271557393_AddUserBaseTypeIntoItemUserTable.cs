namespace POEItemFilter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserBaseTypeIntoItemUserTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemUsers", "UserBaseType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemUsers", "UserBaseType");
        }
    }
}
