namespace JobPortalSharp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.JobApplications", "JobPostId", "dbo.JobPosts");
            RenameTable(name: "dbo.JobApplications", newName: "JobApplicationHeaders");
            DropIndex("dbo.JobApplicationHeaders", new[] { "JobPostId" });
            RenameColumn(table: "dbo.JobApplicationHeaders", name: "JobPostId", newName: "JobPost_Id");
            CreateTable(
                "dbo.JobApplicationDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobPostId = c.Int(nullable: false),
                        JobApplicationHeaderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JobApplicationHeaders", t => t.JobApplicationHeaderId, cascadeDelete: true)
                .ForeignKey("dbo.JobPosts", t => t.JobPostId, cascadeDelete: true)
                .Index(t => t.JobPostId)
                .Index(t => t.JobApplicationHeaderId);
            
            AlterColumn("dbo.JobApplicationHeaders", "JobPost_Id", c => c.Int());
            CreateIndex("dbo.JobApplicationHeaders", "JobPost_Id");
            AddForeignKey("dbo.JobApplicationHeaders", "JobPost_Id", "dbo.JobPosts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobApplicationHeaders", "JobPost_Id", "dbo.JobPosts");
            DropForeignKey("dbo.JobApplicationDetails", "JobPostId", "dbo.JobPosts");
            DropForeignKey("dbo.JobApplicationDetails", "JobApplicationHeaderId", "dbo.JobApplicationHeaders");
            DropIndex("dbo.JobApplicationDetails", new[] { "JobApplicationHeaderId" });
            DropIndex("dbo.JobApplicationDetails", new[] { "JobPostId" });
            DropIndex("dbo.JobApplicationHeaders", new[] { "JobPost_Id" });
            AlterColumn("dbo.JobApplicationHeaders", "JobPost_Id", c => c.Int(nullable: false));
            DropTable("dbo.JobApplicationDetails");
            RenameColumn(table: "dbo.JobApplicationHeaders", name: "JobPost_Id", newName: "JobPostId");
            CreateIndex("dbo.JobApplicationHeaders", "JobPostId");
            AddForeignKey("dbo.JobApplications", "JobPostId", "dbo.JobPosts", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.JobApplicationHeaders", newName: "JobApplications");
        }
    }
}
