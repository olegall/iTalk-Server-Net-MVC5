namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb14 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ImageSrc = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subcategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.JuridicConsultants", "CategoryId", c => c.Long(nullable: false));
            AddColumn("dbo.PrivateConsultants", "CategoryId", c => c.Long(nullable: false));
            AddColumn("dbo.Services", "Cost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Services", "Cost");
            DropColumn("dbo.PrivateConsultants", "CategoryId");
            DropColumn("dbo.JuridicConsultants", "CategoryId");
            DropTable("dbo.Subcategories");
            DropTable("dbo.Categories");
        }
    }
}
