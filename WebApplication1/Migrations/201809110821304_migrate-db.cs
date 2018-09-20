namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.GalleryImages");
            AddColumn("dbo.GalleryImages", "Bytes", c => c.Binary());
            AddColumn("dbo.GalleryImages", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.GalleryImages", "FileName", c => c.String(maxLength: 250));
            AddColumn("dbo.GalleryImages", "Size", c => c.Long(nullable: false));
            AlterColumn("dbo.GalleryImages", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.GalleryImages", "Id");
            DropColumn("dbo.GalleryImages", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GalleryImages", "Name", c => c.String());
            DropPrimaryKey("dbo.GalleryImages");
            AlterColumn("dbo.GalleryImages", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.GalleryImages", "Size");
            DropColumn("dbo.GalleryImages", "FileName");
            DropColumn("dbo.GalleryImages", "Date");
            DropColumn("dbo.GalleryImages", "Bytes");
            AddPrimaryKey("dbo.GalleryImages", "Id");
        }
    }
}
