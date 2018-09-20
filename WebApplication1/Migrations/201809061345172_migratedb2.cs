namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChatMsgs", "OrderId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChatMsgs", "OrderId");
        }
    }
}
