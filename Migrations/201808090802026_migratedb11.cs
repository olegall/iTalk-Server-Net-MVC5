namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb11 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JuridicConsultants", "Phone", c => c.String());
            AlterColumn("dbo.PrivateConsultants", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PrivateConsultants", "Phone", c => c.String(maxLength: 20));
            AlterColumn("dbo.JuridicConsultants", "Phone", c => c.String(maxLength: 20));
        }
    }
}
