namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb30 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoryImages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CategoryId = c.Long(nullable: false),
                        Bytes = c.Binary(),
                        Date = c.DateTime(nullable: false),
                        FileName = c.String(maxLength: 250),
                        Size = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CategoryImages");
        }
    }
}
