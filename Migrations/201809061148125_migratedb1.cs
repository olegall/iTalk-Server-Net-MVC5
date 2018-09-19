namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatMsgs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SenderId = c.Long(nullable: false),
                        SenderName = c.String(),
                        Text = c.String(),
                        Image = c.String(),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ChatMsgs");
        }
    }
}
