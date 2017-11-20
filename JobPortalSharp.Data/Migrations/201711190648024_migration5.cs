namespace JobPortalSharp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        Notes = c.String(),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        LastUpdatedById = c.String(maxLength: 128),
                        LastUpdatedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 128),
                        DeletedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedById)
                .ForeignKey("dbo.AspNetUsers", t => t.LastUpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.LastUpdatedById)
                .Index(t => t.DeletedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Settings", "LastUpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Settings", "DeletedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Settings", "CreatedById", "dbo.AspNetUsers");
            DropIndex("dbo.Settings", new[] { "DeletedById" });
            DropIndex("dbo.Settings", new[] { "LastUpdatedById" });
            DropIndex("dbo.Settings", new[] { "CreatedById" });
            DropTable("dbo.Settings");
        }
    }
}
