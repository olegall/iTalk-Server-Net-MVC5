namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb12 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.JuridicConsultants");
            DropPrimaryKey("dbo.PrivateConsultants");
            AddColumn("dbo.GalleryImages", "ConsultantId", c => c.Long(nullable: false));
            AlterColumn("dbo.JuridicConsultants", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.PrivateConsultants", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.JuridicConsultants", "Id");
            AddPrimaryKey("dbo.PrivateConsultants", "Id");
            DropColumn("dbo.GalleryImages", "Phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GalleryImages", "Phone", c => c.String());
            DropPrimaryKey("dbo.PrivateConsultants");
            DropPrimaryKey("dbo.JuridicConsultants");
            AlterColumn("dbo.PrivateConsultants", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.JuridicConsultants", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.GalleryImages", "ConsultantId");
            AddPrimaryKey("dbo.PrivateConsultants", "Id");
            AddPrimaryKey("dbo.JuridicConsultants", "Id");
        }
    }
}
