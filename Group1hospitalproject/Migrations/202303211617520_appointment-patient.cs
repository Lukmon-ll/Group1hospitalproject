namespace Group1hospitalproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appointmentpatient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "PatientID", c => c.Int(nullable: false));
            CreateIndex("dbo.Appointments", "PatientID");
            AddForeignKey("dbo.Appointments", "PatientID", "dbo.Patients", "PatientID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "PatientID", "dbo.Patients");
            DropIndex("dbo.Appointments", new[] { "PatientID" });
            DropColumn("dbo.Appointments", "PatientID");
        }
    }
}
