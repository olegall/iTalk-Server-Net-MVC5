namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratedb : DbMigration
    {
        public override void Up()
        {
            //DropPrimaryKey("dbo.Feedbacks");
            //DropPrimaryKey("dbo.PrivateConsultants");
            //DropPrimaryKey("dbo.Services");



            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateTable(
                "dbo.ChatMsgs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        OrderId = c.Long(nullable: false),
                        SenderId = c.Long(nullable: false),
                        SenderName = c.String(),
                        Text = c.String(),
                        Image = c.String(),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        SocketId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ConsultantImages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ConsultantId = c.Long(nullable: false),
                        FileTypeId = c.Long(nullable: false),
                        Bytes = c.Binary(),
                        Date = c.DateTime(nullable: false),
                        FileName = c.String(maxLength: 250),
                        Size = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ConsultationTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Favorites",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ClientId = c.Long(nullable: false),
                        ConsultantId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GalleryImages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ConsultantId = c.Long(nullable: false),
                        Bytes = c.Binary(),
                        Date = c.DateTime(nullable: false),
                        FileName = c.String(maxLength: 250),
                        Size = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JuridicConsultants",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LTDTitle = c.String(maxLength: 50),
                        OGRN = c.String(maxLength: 100),
                        INN = c.String(maxLength: 50),
                        SiteUrl = c.String(maxLength: 50),
                        LogoName = c.String(),
                        OGRNCertificate = c.String(),
                        BankAccountDetails = c.String(),
                        Phone = c.String(),
                        ModerationStatusId = c.Long(nullable: false),
                        Email = c.String(maxLength: 50),
                        YandexWalletNum = c.String(maxLength: 100),
                        ITalkCommittee = c.Decimal(nullable: false, storeType: "money"),
                        ITalkDebts = c.Decimal(nullable: false, storeType: "money"),
                        ITalkEarnedMoney = c.Decimal(nullable: false, storeType: "money"),
                        Rating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccountNumber = c.String(maxLength: 50),
                        ServicesDescription = c.String(),
                        Free = c.Boolean(nullable: false),
                        FreeDate = c.DateTime(nullable: false),
                        SocketId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Number = c.Long(nullable: false),
                        ClientId = c.Long(nullable: false),
                        ConsultantId = c.Long(nullable: false),
                        ServiceId = c.Long(nullable: false),
                        StatusCode = c.Int(nullable: false),
                        PaymentStatusId = c.Long(nullable: false),
                        ConsultationTypeId = c.Long(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Comment = c.String(),
                        ClientsGradeToConsultant = c.Int(nullable: false),
                        ConfirmedByClient = c.Boolean(nullable: false),
                        ConfirmedByConsultant = c.Boolean(nullable: false),
                        Sum = c.Decimal(nullable: false, storeType: "money"),
                        YandexWalletNum = c.String(),
                        BankAccountDetails = c.String(),
                        ITalkCommittee = c.Decimal(nullable: false, storeType: "money"),
                        RequestDescription = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PaymentStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceImages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ServiceId = c.Long(nullable: false),
                        Bytes = c.Binary(),
                        Date = c.DateTime(nullable: false),
                        FileName = c.String(maxLength: 250),
                        Size = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subcategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Title = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            //AddColumn("dbo.Feedbacks", "ClientId", c => c.Long(nullable: false));
            //AddColumn("dbo.PrivateConsultants", "ProfileImageName", c => c.String());
            //AddColumn("dbo.PrivateConsultants", "PhotoName", c => c.String());
            //AddColumn("dbo.PrivateConsultants", "PassportScanName", c => c.String());
            //AddColumn("dbo.PrivateConsultants", "YandexWalletNum", c => c.String(maxLength: 100));
            //AddColumn("dbo.PrivateConsultants", "AccountNumber", c => c.String(maxLength: 50));
            //AddColumn("dbo.PrivateConsultants", "ServicesDescription", c => c.String());
            //AddColumn("dbo.PrivateConsultants", "Free", c => c.Boolean(nullable: false));
            //AddColumn("dbo.PrivateConsultants", "FreeDate", c => c.DateTime(nullable: false));
            //AddColumn("dbo.PrivateConsultants", "SocketId", c => c.Guid(nullable: false));
            //AddColumn("dbo.Services", "CategoryId", c => c.Long(nullable: false));
            //AddColumn("dbo.Services", "SubcategoryId", c => c.Long(nullable: false));
            //AddColumn("dbo.Services", "Phone", c => c.String());
            //AddColumn("dbo.Services", "Cost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            //AddColumn("dbo.Services", "Deleted", c => c.Boolean(nullable: false));
            //AddColumn("dbo.Services", "Available", c => c.Boolean(nullable: false));
            //AddColumn("dbo.Services", "AvailablePeriod", c => c.Int(nullable: false));
            //AlterColumn("dbo.Feedbacks", "Id", c => c.Long(nullable: false, identity: true));
            //AlterColumn("dbo.Feedbacks", "ConsultantId", c => c.Long(nullable: false));
            //AlterColumn("dbo.PrivateConsultants", "Id", c => c.Long(nullable: false, identity: true));
            //AlterColumn("dbo.PrivateConsultants", "ModerationStatusId", c => c.Long(nullable: false));
            //AlterColumn("dbo.PrivateConsultants", "Phone", c => c.String());
            //AlterColumn("dbo.PrivateConsultants", "Name", c => c.String(maxLength: 20));
            //AlterColumn("dbo.PrivateConsultants", "Surname", c => c.String(maxLength: 20));
            //AlterColumn("dbo.PrivateConsultants", "Patronymic", c => c.String(maxLength: 20));
            //AlterColumn("dbo.Services", "Id", c => c.Long(nullable: false, identity: true));
            //AlterColumn("dbo.Services", "ConsultantId", c => c.Long(nullable: false));
            //AlterColumn("dbo.Services", "ModerationStatusId", c => c.Long(nullable: false));
            //AlterColumn("dbo.Services", "ServiceCostId", c => c.Long(nullable: false));
            //AddPrimaryKey("dbo.Feedbacks", "Id");
            //AddPrimaryKey("dbo.PrivateConsultants", "Id");
            //AddPrimaryKey("dbo.Services", "Id");
            //DropColumn("dbo.PrivateConsultants", "SectionId");
            //DropColumn("dbo.PrivateConsultants", "YandexMoneyNum");
            //DropColumn("dbo.Services", "ImageSrc");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Services", "ImageSrc", c => c.String());
            AddColumn("dbo.PrivateConsultants", "YandexMoneyNum", c => c.String(maxLength: 100));
            AddColumn("dbo.PrivateConsultants", "SectionId", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Services");
            DropPrimaryKey("dbo.PrivateConsultants");
            DropPrimaryKey("dbo.Feedbacks");
            AlterColumn("dbo.Services", "ServiceCostId", c => c.Int(nullable: false));
            AlterColumn("dbo.Services", "ModerationStatusId", c => c.Int(nullable: false));
            AlterColumn("dbo.Services", "ConsultantId", c => c.Int(nullable: false));
            AlterColumn("dbo.Services", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.PrivateConsultants", "Patronymic", c => c.String());
            AlterColumn("dbo.PrivateConsultants", "Surname", c => c.String());
            AlterColumn("dbo.PrivateConsultants", "Name", c => c.String());
            AlterColumn("dbo.PrivateConsultants", "Phone", c => c.String(maxLength: 20));
            AlterColumn("dbo.PrivateConsultants", "ModerationStatusId", c => c.Int(nullable: false));
            AlterColumn("dbo.PrivateConsultants", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Feedbacks", "ConsultantId", c => c.Int(nullable: false));
            AlterColumn("dbo.Feedbacks", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Services", "AvailablePeriod");
            DropColumn("dbo.Services", "Available");
            DropColumn("dbo.Services", "Deleted");
            DropColumn("dbo.Services", "Cost");
            DropColumn("dbo.Services", "Phone");
            DropColumn("dbo.Services", "SubcategoryId");
            DropColumn("dbo.Services", "CategoryId");
            DropColumn("dbo.PrivateConsultants", "SocketId");
            DropColumn("dbo.PrivateConsultants", "FreeDate");
            DropColumn("dbo.PrivateConsultants", "Free");
            DropColumn("dbo.PrivateConsultants", "ServicesDescription");
            DropColumn("dbo.PrivateConsultants", "AccountNumber");
            DropColumn("dbo.PrivateConsultants", "YandexWalletNum");
            DropColumn("dbo.PrivateConsultants", "PassportScanName");
            DropColumn("dbo.PrivateConsultants", "PhotoName");
            DropColumn("dbo.PrivateConsultants", "ProfileImageName");
            DropColumn("dbo.Feedbacks", "ClientId");
            DropTable("dbo.Subcategories");
            DropTable("dbo.ServiceImages");
            DropTable("dbo.PaymentStatus");
            DropTable("dbo.OrderStatus");
            DropTable("dbo.Orders");
            DropTable("dbo.JuridicConsultants");
            DropTable("dbo.GalleryImages");
            DropTable("dbo.Favorites");
            DropTable("dbo.ConsultationTypes");
            DropTable("dbo.ConsultantImages");
            DropTable("dbo.Clients");
            DropTable("dbo.ChatMsgs");
            DropTable("dbo.ChatActivities");
            DropTable("dbo.CategoryImages");
            DropTable("dbo.Categories");
            AddPrimaryKey("dbo.Services", "Id");
            AddPrimaryKey("dbo.PrivateConsultants", "Id");
            AddPrimaryKey("dbo.Feedbacks", "Id");
        }
    }
}
