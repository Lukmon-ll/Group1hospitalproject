﻿namespace Group1hospitalproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class departments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(),
                        DepartmentDescription = c.String(),
                        DepartmentNews = c.String(),
                    })
                .PrimaryKey(t => t.DepartmentID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Departments");
        }
    }
}
