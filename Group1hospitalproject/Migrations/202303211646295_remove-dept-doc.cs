namespace Group1hospitalproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedeptdoc : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Appointments", "DepartmentID", "dbo.Departments");
            DropIndex("dbo.Appointments", new[] { "DepartmentID" });
            DropColumn("dbo.Appointments", "DepartmentID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "DepartmentID", c => c.Int(nullable: false));
            CreateIndex("dbo.Appointments", "DepartmentID");
            AddForeignKey("dbo.Appointments", "DepartmentID", "dbo.Departments", "DepartmentID", cascadeDelete: true);
        }
    }
}
