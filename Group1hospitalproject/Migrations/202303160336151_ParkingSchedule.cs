namespace Group1hospitalproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ParkingSchedule : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParkingSchedules",
                c => new
                    {
                        ParkingScheduleID = c.Int(nullable: false, identity: true),
                        ParkingSpotID = c.Int(nullable: false),
                        ParkingCarID = c.Int(nullable: false),
                        DateTimeIn = c.DateTime(nullable: false),
                        DateTimeOut = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ParkingScheduleID)
                .ForeignKey("dbo.ParkingCars", t => t.ParkingCarID, cascadeDelete: true)
                .ForeignKey("dbo.ParkingSpots", t => t.ParkingSpotID, cascadeDelete: true)
                .Index(t => t.ParkingSpotID)
                .Index(t => t.ParkingCarID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParkingSchedules", "ParkingSpotID", "dbo.ParkingSpots");
            DropForeignKey("dbo.ParkingSchedules", "ParkingCarID", "dbo.ParkingCars");
            DropIndex("dbo.ParkingSchedules", new[] { "ParkingCarID" });
            DropIndex("dbo.ParkingSchedules", new[] { "ParkingSpotID" });
            DropTable("dbo.ParkingSchedules");
        }
    }
}
