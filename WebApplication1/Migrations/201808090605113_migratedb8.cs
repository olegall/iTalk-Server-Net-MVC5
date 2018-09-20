namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GalleryImages", "Phone", c => c.String());
            AddColumn("dbo.PrivateConsultants", "ProfileImageName", c => c.String());
            DropColumn("dbo.GalleryImages", "ConsultantId");
            DropColumn("dbo.PrivateConsultants", "ProfileImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PrivateConsultants", "ProfileImage", c => c.String());
            AddColumn("dbo.GalleryImages", "ConsultantId", c => c.Int(nullable: false));
            DropColumn("dbo.PrivateConsultants", "ProfileImageName");
            DropColumn("dbo.GalleryImages", "Phone");
        }
    }
}
