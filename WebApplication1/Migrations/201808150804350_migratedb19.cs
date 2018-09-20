namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb19 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JuridicConsultants", "LTDTitle", c => c.String(maxLength: 50));
            AddColumn("dbo.JuridicConsultants", "INN", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JuridicConsultants", "INN");
            DropColumn("dbo.JuridicConsultants", "LTDTitle");
        }
    }
}
