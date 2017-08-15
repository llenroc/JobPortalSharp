namespace JobPortalSharp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false),
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
            
            AddColumn("dbo.Employers", "CountryId", c => c.Int());
            CreateIndex("dbo.Employers", "CountryId");
            AddForeignKey("dbo.Employers", "CountryId", "dbo.Countries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employers", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Countries", "LastUpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Countries", "DeletedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Countries", "CreatedById", "dbo.AspNetUsers");
            DropIndex("dbo.Employers", new[] { "CountryId" });
            DropIndex("dbo.Countries", new[] { "DeletedById" });
            DropIndex("dbo.Countries", new[] { "LastUpdatedById" });
            DropIndex("dbo.Countries", new[] { "CreatedById" });
            DropColumn("dbo.Employers", "CountryId");
            DropTable("dbo.Countries");
        }
    }
}
