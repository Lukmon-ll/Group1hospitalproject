namespace Group1hospitalproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appointdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "AppointDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "AppointDate");
        }
    }
}
