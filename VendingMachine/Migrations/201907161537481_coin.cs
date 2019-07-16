namespace VendingMachine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class coin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Coins", "Disabled", c => c.Boolean(nullable: false));
            DropColumn("dbo.Coins", "IsEnabled");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Coins", "IsEnabled", c => c.Boolean(nullable: false));
            DropColumn("dbo.Coins", "Disabled");
        }
    }
}
