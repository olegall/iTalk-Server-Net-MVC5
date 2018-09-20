namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb17 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Services");
            AlterColumn("dbo.Services", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Services", "ModerationStatusId", c => c.Long(nullable: false));
            AlterColumn("dbo.Services", "ServiceCostId", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.Services", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Services");
            AlterColumn("dbo.Services", "ServiceCostId", c => c.Int(nullable: false));
            AlterColumn("dbo.Services", "ModerationStatusId", c => c.Int(nullable: false));
            AlterColumn("dbo.Services", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Services", "Id");
        }
    }
}
