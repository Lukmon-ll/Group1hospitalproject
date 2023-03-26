namespace Group1hospitalproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ParkingCar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParkingCars",
                c => new
                    {
                        ParkingCarID = c.Int(nullable: false, identity: true),
                        LicencePlate = c.String(),
                        DoctorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ParkingCarID)
                .ForeignKey("dbo.Doctors", t => t.DoctorID, cascadeDelete: true)
                .Index(t => t.DoctorID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParkingCars", "DoctorID", "dbo.Doctors");
            DropIndex("dbo.ParkingCars", new[] { "DoctorID" });
            DropTable("dbo.ParkingCars");
        }
    }
}
