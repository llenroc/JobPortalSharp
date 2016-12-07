namespace JobPortalSharp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobApplicationHeaders", "PhoneNumber", c => c.String());
            AddColumn("dbo.JobApplicationHeaders", "IsNewToTheWorkforce", c => c.Boolean(nullable: false));
            AddColumn("dbo.JobApplicationHeaders", "LastJobTitle", c => c.String());
            AddColumn("dbo.JobApplicationHeaders", "LastJobDateStarted", c => c.DateTime());
            AddColumn("dbo.JobApplicationHeaders", "LastJobCompanyName", c => c.String());
            AddColumn("dbo.JobApplicationHeaders", "IsStillInLastJob", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobApplicationHeaders", "IsStillInLastJob");
            DropColumn("dbo.JobApplicationHeaders", "LastJobCompanyName");
            DropColumn("dbo.JobApplicationHeaders", "LastJobDateStarted");
            DropColumn("dbo.JobApplicationHeaders", "LastJobTitle");
            DropColumn("dbo.JobApplicationHeaders", "IsNewToTheWorkforce");
            DropColumn("dbo.JobApplicationHeaders", "PhoneNumber");
        }
    }
}
