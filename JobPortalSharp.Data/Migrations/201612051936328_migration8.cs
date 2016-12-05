namespace JobPortalSharp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employers", "AddressPostalCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employers", "AddressPostalCode");
        }
    }
}
