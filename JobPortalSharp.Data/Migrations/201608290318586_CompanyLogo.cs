namespace JobPortalSharp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyLogo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employers", "CompanyLogoFileName", c => c.String());
            AddColumn("dbo.Employers", "CompanyLogoSystemFileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employers", "CompanyLogoSystemFileName");
            DropColumn("dbo.Employers", "CompanyLogoFileName");
        }
    }
}
