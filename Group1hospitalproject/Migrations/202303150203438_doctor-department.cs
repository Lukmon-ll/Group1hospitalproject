namespace Group1hospitalproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    

    public partial class doctordepartment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "DepartmentID", c => c.Int(nullable: false));
            CreateIndex("dbo.Doctors", "DepartmentID");
            AddForeignKey("dbo.Doctors", "DepartmentID", "dbo.Departments", "DepartmentID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Doctors", "DepartmentID", "dbo.Departments");
            DropIndex("dbo.Doctors", new[] { "DepartmentID" });
            DropColumn("dbo.Doctors", "DepartmentID");
        }
    }
}
