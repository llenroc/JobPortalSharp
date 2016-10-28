namespace JobPortalSharp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobSelections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Notes = c.String(),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        LastUpdatedById = c.String(maxLength: 128),
                        LastUpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById)
                .ForeignKey("dbo.AspNetUsers", t => t.LastUpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.LastUpdatedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobSelections", "LastUpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobSelections", "CreatedById", "dbo.AspNetUsers");
            DropIndex("dbo.JobSelections", new[] { "LastUpdatedById" });
            DropIndex("dbo.JobSelections", new[] { "CreatedById" });
            DropTable("dbo.JobSelections");
        }
    }
}
