namespace JobPortalSharp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Applicants", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Applicants", "ApplicationUserId");
            AddForeignKey("dbo.Applicants", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applicants", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Applicants", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Applicants", "ApplicationUserId", c => c.String());
        }
    }
}
