namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GalleryImages", "ImageContainerId", c => c.Int(nullable: false));
            AddColumn("dbo.GalleryImages", "Name", c => c.String());
            DropColumn("dbo.GalleryImages", "Text");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GalleryImages", "Text", c => c.String());
            DropColumn("dbo.GalleryImages", "Name");
            DropColumn("dbo.GalleryImages", "ImageContainerId");
        }
    }
}
