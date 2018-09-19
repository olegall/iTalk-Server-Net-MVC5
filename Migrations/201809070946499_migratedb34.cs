namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb34 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatActivities",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ConnecteeId = c.Long(nullable: false),
                        OrderId = c.Long(nullable: false),
                        SocketId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ChatActivities");
        }
    }
}
