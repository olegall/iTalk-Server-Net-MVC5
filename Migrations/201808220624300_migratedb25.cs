namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb25 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "DateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Orders", "Date");
            DropColumn("dbo.Orders", "СlientConsultationTime");
            DropColumn("dbo.Orders", "FinalConsultationTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "FinalConsultationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "СlientConsultationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Orders", "DateTime");
        }
    }
}
