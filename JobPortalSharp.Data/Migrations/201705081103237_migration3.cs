namespace JobPortalSharp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobPosts", "Paid", c => c.Boolean(nullable: false));
            AddColumn("dbo.JobPosts", "StripeToken", c => c.String());
            AddColumn("dbo.JobPosts", "StripeEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobPosts", "StripeEmail");
            DropColumn("dbo.JobPosts", "StripeToken");
            DropColumn("dbo.JobPosts", "Paid");
        }
    }
}
