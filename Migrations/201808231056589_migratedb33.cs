namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb33 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Services", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Subcategories", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subcategories", "Deleted");
            DropColumn("dbo.Services", "Deleted");
            DropColumn("dbo.Categories", "Deleted");
        }
    }
}