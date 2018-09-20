namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GalleryImages", "ConsultantId", c => c.Int(nullable: false));
            AddColumn("dbo.PrivateConsultants", "ProfileImage", c => c.String());
            DropColumn("dbo.GalleryImages", "ImageContainerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GalleryImages", "ImageContainerId", c => c.Int(nullable: false));
            DropColumn("dbo.PrivateConsultants", "ProfileImage");
            DropColumn("dbo.GalleryImages", "ConsultantId");
        }
    }
}
