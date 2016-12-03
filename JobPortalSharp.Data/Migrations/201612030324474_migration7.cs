namespace JobPortalSharp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.JobApplicationHeaders", "JobPost_Id", "dbo.JobPosts");
            DropIndex("dbo.JobApplicationHeaders", new[] { "JobPost_Id" });
            DropColumn("dbo.JobApplicationHeaders", "JobPost_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JobApplicationHeaders", "JobPost_Id", c => c.Int());
            CreateIndex("dbo.JobApplicationHeaders", "JobPost_Id");
            AddForeignKey("dbo.JobApplicationHeaders", "JobPost_Id", "dbo.JobPosts", "Id");
        }
    }
}
