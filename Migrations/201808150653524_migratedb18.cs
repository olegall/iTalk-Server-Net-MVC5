namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb18 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Favorites",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ClientId = c.Long(nullable: false),
                        ConsultantId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.JuridicConsultants", "Free", c => c.Boolean(nullable: false));
            AddColumn("dbo.JuridicConsultants", "FreeDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PrivateConsultants", "Free", c => c.Boolean(nullable: false));
            AddColumn("dbo.PrivateConsultants", "FreeDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PrivateConsultants", "FreeDate");
            DropColumn("dbo.PrivateConsultants", "Free");
            DropColumn("dbo.JuridicConsultants", "FreeDate");
            DropColumn("dbo.JuridicConsultants", "Free");
            DropTable("dbo.Favorites");
        }
    }
}
