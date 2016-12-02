namespace JobPortalSharp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobPosts", "AddressStreet", c => c.String());
            AddColumn("dbo.JobPosts", "AddressTown", c => c.String());
            AddColumn("dbo.JobPosts", "AddressState", c => c.String());
            AddColumn("dbo.JobPosts", "AddressCountry", c => c.String());
            AddColumn("dbo.JobPosts", "AddressLongitude", c => c.Double());
            AddColumn("dbo.JobPosts", "AddressLatitude", c => c.Double());
            DropColumn("dbo.JobPosts", "Location");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JobPosts", "Location", c => c.String());
            DropColumn("dbo.JobPosts", "AddressLatitude");
            DropColumn("dbo.JobPosts", "AddressLongitude");
            DropColumn("dbo.JobPosts", "AddressCountry");
            DropColumn("dbo.JobPosts", "AddressState");
            DropColumn("dbo.JobPosts", "AddressTown");
            DropColumn("dbo.JobPosts", "AddressStreet");
        }
    }
}
