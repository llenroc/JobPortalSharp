namespace JobPortalSharp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicants", "ApplicationUserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applicants", "ApplicationUserId");
        }
    }
}
