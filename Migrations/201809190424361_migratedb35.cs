namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb35 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Services", "AvailablePeriod", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Services", "AvailablePeriod");
        }
    }
}
