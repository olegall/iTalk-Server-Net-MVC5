namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb32 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "RequestDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "RequestDescription");
        }
    }
}
