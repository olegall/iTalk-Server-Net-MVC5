namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb26 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "PayBefore");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "PayBefore", c => c.DateTime(nullable: false));
        }
    }
}
