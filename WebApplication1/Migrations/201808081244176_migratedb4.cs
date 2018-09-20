namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.PrivateConsultants", "ImageId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PrivateConsultants", "ImageId");
            DropTable("dbo.OrderStatus");
        }
    }
}
