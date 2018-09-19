namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb10 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.JuridicConsultants");
            DropPrimaryKey("dbo.PrivateConsultants");
            AlterColumn("dbo.JuridicConsultants", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.PrivateConsultants", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.JuridicConsultants", "Id");
            AddPrimaryKey("dbo.PrivateConsultants", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.PrivateConsultants");
            DropPrimaryKey("dbo.JuridicConsultants");
            AlterColumn("dbo.PrivateConsultants", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.JuridicConsultants", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.PrivateConsultants", "Id");
            AddPrimaryKey("dbo.JuridicConsultants", "Id");
        }
    }
}
