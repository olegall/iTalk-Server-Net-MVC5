namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb16 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Services", "ConsultantId", c => c.Long(nullable: false));
            DropColumn("dbo.JuridicConsultants", "ServiceId");
            DropColumn("dbo.PrivateConsultants", "ServiceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PrivateConsultants", "ServiceId", c => c.Long(nullable: false));
            AddColumn("dbo.JuridicConsultants", "ServiceId", c => c.Long(nullable: false));
            DropColumn("dbo.Services", "ConsultantId");
        }
    }
}
