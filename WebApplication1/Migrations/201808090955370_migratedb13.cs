namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb13 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PrivateConsultants", "ImageId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PrivateConsultants", "ImageId", c => c.Long(nullable: false));
        }
    }
}
