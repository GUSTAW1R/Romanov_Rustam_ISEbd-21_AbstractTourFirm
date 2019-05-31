namespace AbstractTourFirm___ServiceImplementsDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Login = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        TravelId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        Sum = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateImplement = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Travels", t => t.TravelId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.TravelId);
            
            CreateTable(
                "dbo.Travels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name_Travel = c.String(nullable: false),
                        Final_Cost = c.Int(nullable: false),
                        Taxi = c.Boolean(nullable: false),
                        AllInclusive = c.Boolean(nullable: false),
                        Private_Guide = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TourForTravels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TravelId = c.Int(nullable: false),
                        TourId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        Date_Start = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tours", t => t.TravelId, cascadeDelete: true)
                .ForeignKey("dbo.Travels", t => t.TravelId, cascadeDelete: true)
                .Index(t => t.TravelId);
            
            CreateTable(
                "dbo.Tours",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourName = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        Cost = c.Int(nullable: false),
                        Season = c.String(),
                        IsCredit = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TourForTravels", "TravelId", "dbo.Travels");
            DropForeignKey("dbo.TourForTravels", "TravelId", "dbo.Tours");
            DropForeignKey("dbo.Orders", "TravelId", "dbo.Travels");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropIndex("dbo.TourForTravels", new[] { "TravelId" });
            DropIndex("dbo.Orders", new[] { "TravelId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropTable("dbo.Tours");
            DropTable("dbo.TourForTravels");
            DropTable("dbo.Travels");
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
        }
    }
}
