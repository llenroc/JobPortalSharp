namespace JobPortalSharp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicants", "CreatedById", c => c.String(maxLength: 128));
            AddColumn("dbo.Applicants", "CreatedDate", c => c.DateTime());
            AddColumn("dbo.Applicants", "LastUpdatedById", c => c.String(maxLength: 128));
            AddColumn("dbo.Applicants", "LastUpdatedDate", c => c.DateTime());
            AddColumn("dbo.JobPosts", "PostDate", c => c.DateTime());
            AddColumn("dbo.JobPosts", "CreatedById", c => c.String(maxLength: 128));
            AddColumn("dbo.JobPosts", "CreatedDate", c => c.DateTime());
            AddColumn("dbo.JobPosts", "LastUpdatedById", c => c.String(maxLength: 128));
            AddColumn("dbo.JobPosts", "LastUpdatedDate", c => c.DateTime());
            AddColumn("dbo.Employers", "CreatedById", c => c.String(maxLength: 128));
            AddColumn("dbo.Employers", "CreatedDate", c => c.DateTime());
            AddColumn("dbo.Employers", "LastUpdatedById", c => c.String(maxLength: 128));
            AddColumn("dbo.Employers", "LastUpdatedDate", c => c.DateTime());
            AddColumn("dbo.EmploymentTypes", "CreatedById", c => c.String(maxLength: 128));
            AddColumn("dbo.EmploymentTypes", "CreatedDate", c => c.DateTime());
            AddColumn("dbo.EmploymentTypes", "LastUpdatedById", c => c.String(maxLength: 128));
            AddColumn("dbo.EmploymentTypes", "LastUpdatedDate", c => c.DateTime());
            AddColumn("dbo.Industries", "CreatedById", c => c.String(maxLength: 128));
            AddColumn("dbo.Industries", "CreatedDate", c => c.DateTime());
            AddColumn("dbo.Industries", "LastUpdatedById", c => c.String(maxLength: 128));
            AddColumn("dbo.Industries", "LastUpdatedDate", c => c.DateTime());
            CreateIndex("dbo.Applicants", "CreatedById");
            CreateIndex("dbo.Applicants", "LastUpdatedById");
            CreateIndex("dbo.JobPosts", "CreatedById");
            CreateIndex("dbo.JobPosts", "LastUpdatedById");
            CreateIndex("dbo.Employers", "CreatedById");
            CreateIndex("dbo.Employers", "LastUpdatedById");
            CreateIndex("dbo.EmploymentTypes", "CreatedById");
            CreateIndex("dbo.EmploymentTypes", "LastUpdatedById");
            CreateIndex("dbo.Industries", "CreatedById");
            CreateIndex("dbo.Industries", "LastUpdatedById");
            AddForeignKey("dbo.Applicants", "CreatedById", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Applicants", "LastUpdatedById", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.JobPosts", "CreatedById", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Employers", "CreatedById", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Employers", "LastUpdatedById", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.EmploymentTypes", "CreatedById", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.EmploymentTypes", "LastUpdatedById", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Industries", "CreatedById", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Industries", "LastUpdatedById", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.JobPosts", "LastUpdatedById", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Applicants", "LastUpdated");
            DropColumn("dbo.Applicants", "LastUpdatedBy");
            DropColumn("dbo.JobPosts", "CreateDate");
            DropColumn("dbo.JobPosts", "LastUpdated");
            DropColumn("dbo.JobPosts", "LastUpdatedBy");
            DropColumn("dbo.Employers", "LastUpdated");
            DropColumn("dbo.Employers", "LastUpdatedBy");
            DropColumn("dbo.EmploymentTypes", "LastUpdated");
            DropColumn("dbo.EmploymentTypes", "LastUpdatedBy");
            DropColumn("dbo.Industries", "LastUpdated");
            DropColumn("dbo.Industries", "LastUpdatedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Industries", "LastUpdatedBy", c => c.String());
            AddColumn("dbo.Industries", "LastUpdated", c => c.DateTime());
            AddColumn("dbo.EmploymentTypes", "LastUpdatedBy", c => c.String());
            AddColumn("dbo.EmploymentTypes", "LastUpdated", c => c.DateTime());
            AddColumn("dbo.Employers", "LastUpdatedBy", c => c.String());
            AddColumn("dbo.Employers", "LastUpdated", c => c.DateTime());
            AddColumn("dbo.JobPosts", "LastUpdatedBy", c => c.String());
            AddColumn("dbo.JobPosts", "LastUpdated", c => c.DateTime());
            AddColumn("dbo.JobPosts", "CreateDate", c => c.DateTime());
            AddColumn("dbo.Applicants", "LastUpdatedBy", c => c.String());
            AddColumn("dbo.Applicants", "LastUpdated", c => c.DateTime());
            DropForeignKey("dbo.JobPosts", "LastUpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Industries", "LastUpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Industries", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.EmploymentTypes", "LastUpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.EmploymentTypes", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Employers", "LastUpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Employers", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobPosts", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Applicants", "LastUpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Applicants", "CreatedById", "dbo.AspNetUsers");
            DropIndex("dbo.Industries", new[] { "LastUpdatedById" });
            DropIndex("dbo.Industries", new[] { "CreatedById" });
            DropIndex("dbo.EmploymentTypes", new[] { "LastUpdatedById" });
            DropIndex("dbo.EmploymentTypes", new[] { "CreatedById" });
            DropIndex("dbo.Employers", new[] { "LastUpdatedById" });
            DropIndex("dbo.Employers", new[] { "CreatedById" });
            DropIndex("dbo.JobPosts", new[] { "LastUpdatedById" });
            DropIndex("dbo.JobPosts", new[] { "CreatedById" });
            DropIndex("dbo.Applicants", new[] { "LastUpdatedById" });
            DropIndex("dbo.Applicants", new[] { "CreatedById" });
            DropColumn("dbo.Industries", "LastUpdatedDate");
            DropColumn("dbo.Industries", "LastUpdatedById");
            DropColumn("dbo.Industries", "CreatedDate");
            DropColumn("dbo.Industries", "CreatedById");
            DropColumn("dbo.EmploymentTypes", "LastUpdatedDate");
            DropColumn("dbo.EmploymentTypes", "LastUpdatedById");
            DropColumn("dbo.EmploymentTypes", "CreatedDate");
            DropColumn("dbo.EmploymentTypes", "CreatedById");
            DropColumn("dbo.Employers", "LastUpdatedDate");
            DropColumn("dbo.Employers", "LastUpdatedById");
            DropColumn("dbo.Employers", "CreatedDate");
            DropColumn("dbo.Employers", "CreatedById");
            DropColumn("dbo.JobPosts", "LastUpdatedDate");
            DropColumn("dbo.JobPosts", "LastUpdatedById");
            DropColumn("dbo.JobPosts", "CreatedDate");
            DropColumn("dbo.JobPosts", "CreatedById");
            DropColumn("dbo.JobPosts", "PostDate");
            DropColumn("dbo.Applicants", "LastUpdatedDate");
            DropColumn("dbo.Applicants", "LastUpdatedById");
            DropColumn("dbo.Applicants", "CreatedDate");
            DropColumn("dbo.Applicants", "CreatedById");
        }
    }
}
