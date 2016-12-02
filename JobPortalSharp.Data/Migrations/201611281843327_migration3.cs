namespace JobPortalSharp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobPosts", "LocationSameAsEmployer", c => c.Boolean(nullable: false));
            AddColumn("dbo.JobPosts", "Location", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobPosts", "Location");
            DropColumn("dbo.JobPosts", "LocationSameAsEmployer");
        }
    }
}
