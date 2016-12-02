namespace JobPortalSharp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employers", "StateId", "dbo.States");
            DropForeignKey("dbo.Suburbs", "StateId", "dbo.States");
            DropIndex("dbo.Employers", new[] { "StateId" });
            DropIndex("dbo.Suburbs", new[] { "StateId" });
            AddColumn("dbo.Employers", "AddressStreet", c => c.String());
            AddColumn("dbo.Employers", "AddressTown", c => c.String());
            AddColumn("dbo.Employers", "AddressState", c => c.String());
            AddColumn("dbo.Employers", "AddressCountry", c => c.String());
            AddColumn("dbo.Employers", "AddressLongitude", c => c.Double());
            AddColumn("dbo.Employers", "AddressLatitude", c => c.Double());
            DropColumn("dbo.Employers", "CompanyName");
            DropColumn("dbo.Employers", "CompanyAddress1");
            DropColumn("dbo.Employers", "CompanyAddress2");
            DropColumn("dbo.Employers", "StateId");
            DropTable("dbo.States");
            DropTable("dbo.Suburbs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Suburbs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StateId = c.Int(),
                        Name = c.String(),
                        Lattitude = c.Double(),
                        Longitude = c.Double(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShortName = c.String(),
                        Name = c.String(),
                        Lattitude = c.Double(),
                        Longitude = c.Double(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Employers", "StateId", c => c.Int(nullable: false));
            AddColumn("dbo.Employers", "CompanyAddress2", c => c.String());
            AddColumn("dbo.Employers", "CompanyAddress1", c => c.String());
            AddColumn("dbo.Employers", "CompanyName", c => c.Int(nullable: false));
            DropColumn("dbo.Employers", "AddressLatitude");
            DropColumn("dbo.Employers", "AddressLongitude");
            DropColumn("dbo.Employers", "AddressCountry");
            DropColumn("dbo.Employers", "AddressState");
            DropColumn("dbo.Employers", "AddressTown");
            DropColumn("dbo.Employers", "AddressStreet");
            CreateIndex("dbo.Suburbs", "StateId");
            CreateIndex("dbo.Employers", "StateId");
            AddForeignKey("dbo.Suburbs", "StateId", "dbo.States", "Id");
            AddForeignKey("dbo.Employers", "StateId", "dbo.States", "Id", cascadeDelete: true);
        }
    }
}
