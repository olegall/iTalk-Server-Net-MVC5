namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Services", "Phone", c => c.String());
            DropColumn("dbo.Services", "ConsultantId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Services", "ConsultantId", c => c.Int(nullable: false));
            DropColumn("dbo.Services", "Phone");
        }
    }
}
