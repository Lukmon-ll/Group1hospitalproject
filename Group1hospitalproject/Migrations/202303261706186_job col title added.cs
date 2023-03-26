namespace Group1hospitalproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobcoltitleadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "JobTitle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "JobTitle");
        }
    }
}
