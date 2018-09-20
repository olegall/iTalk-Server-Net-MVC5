namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Description", c => c.String());
            DropColumn("dbo.Services", "ImageSrc");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Services", "ImageSrc", c => c.String());
            DropColumn("dbo.Categories", "Description");
        }
    }
}
