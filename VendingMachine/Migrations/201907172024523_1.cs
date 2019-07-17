namespace VendingMachine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Drinks", "Price", c => c.Int(nullable: false));
            DropColumn("dbo.Drinks", "Value");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Drinks", "Value", c => c.Int(nullable: false));
            DropColumn("dbo.Drinks", "Price");
        }
    }
}
