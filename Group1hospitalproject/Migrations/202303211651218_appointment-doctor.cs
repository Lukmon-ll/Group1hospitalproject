namespace Group1hospitalproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appointmentdoctor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "DoctorID", c => c.Int(nullable: false));
            CreateIndex("dbo.Appointments", "DoctorID");
            AddForeignKey("dbo.Appointments", "DoctorID", "dbo.Doctors", "DoctorID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "DoctorID", "dbo.Doctors");
            DropIndex("dbo.Appointments", new[] { "DoctorID" });
            DropColumn("dbo.Appointments", "DoctorID");
        }
    }
}
