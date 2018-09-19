namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb23 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Number = c.Long(nullable: false),
                        ClientId = c.Long(nullable: false),
                        ConsultantId = c.Long(nullable: false),
                        ServiceId = c.Long(nullable: false),
                        StatusId = c.Long(nullable: false),
                        PaymentStatusId = c.Long(nullable: false),
                        ConsultationTypeId = c.Long(nullable: false),
                        Date = c.DateTime(nullable: false),
                        СlientConsultationTime = c.DateTime(nullable: false),
                        FinalConsultationTime = c.DateTime(nullable: false),
                        Comment = c.String(),
                        ClientsGradeToConsultant = c.Int(nullable: false),
                        ConfirmedByClient = c.Boolean(nullable: false),
                        ConfirmedByConsultant = c.Boolean(nullable: false),
                        Sum = c.Decimal(nullable: false, storeType: "money"),
                        YandexWalletNum = c.String(),
                        BankAccountDetails = c.String(),
                        ITalkCommittee = c.Decimal(nullable: false, storeType: "money"),
                        PayBefore = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Orders");
        }
    }
}
