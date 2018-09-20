namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JuridicConsultants",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SubcategoryId = c.Long(nullable: false),
                        ServiceId = c.Long(nullable: false),
                        ModerationStatusId = c.Long(nullable: false),
                        OGRN = c.String(maxLength: 100),
                        Phone = c.String(maxLength: 20),
                        Email = c.String(maxLength: 50),
                        SiteUrl = c.String(maxLength: 50),
                        LogoName = c.String(),
                        OGRNCertificate = c.String(),
                        BankAccountDetails = c.String(),
                        YandexWalletNum = c.String(maxLength: 100),
                        ITalkCommittee = c.Decimal(nullable: false, storeType: "money"),
                        ITalkDebts = c.Decimal(nullable: false, storeType: "money"),
                        ITalkEarnedMoney = c.Decimal(nullable: false, storeType: "money"),
                        Rating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccountNumber = c.String(maxLength: 50),
                        ServicesDescription = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.JuridicConsultants");
        }
    }
}
