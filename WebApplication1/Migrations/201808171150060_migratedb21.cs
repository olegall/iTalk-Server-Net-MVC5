namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConsultantImages", "FileTypeId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConsultantImages", "FileTypeId");
        }
    }
}
