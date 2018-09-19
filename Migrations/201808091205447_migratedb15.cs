namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Services", "CategoryId", c => c.Long(nullable: false));
            AddColumn("dbo.Services", "SubcategoryId", c => c.Long(nullable: false));
            DropColumn("dbo.JuridicConsultants", "CategoryId");
            DropColumn("dbo.JuridicConsultants", "SubcategoryId");
            DropColumn("dbo.PrivateConsultants", "CategoryId");
            DropColumn("dbo.PrivateConsultants", "SubcategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PrivateConsultants", "SubcategoryId", c => c.Long(nullable: false));
            AddColumn("dbo.PrivateConsultants", "CategoryId", c => c.Long(nullable: false));
            AddColumn("dbo.JuridicConsultants", "SubcategoryId", c => c.Long(nullable: false));
            AddColumn("dbo.JuridicConsultants", "CategoryId", c => c.Long(nullable: false));
            DropColumn("dbo.Services", "SubcategoryId");
            DropColumn("dbo.Services", "CategoryId");
        }
    }
}
