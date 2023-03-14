namespace Group1hospitalproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class parking_spots : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParkingSpots",
                c => new
                    {
                        ParkingSpotID = c.Int(nullable: false, identity: true),
                        SpotNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ParkingSpotID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ParkingSpots");
        }
    }
}
