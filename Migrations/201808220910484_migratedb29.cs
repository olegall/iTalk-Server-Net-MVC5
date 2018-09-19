namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb29 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Categories", "ImageSrc");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "ImageSrc", c => c.String());
        }
    }
}