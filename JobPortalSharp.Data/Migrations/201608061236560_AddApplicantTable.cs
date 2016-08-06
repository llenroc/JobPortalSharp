namespace JobPortalSharp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApplicantTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applicants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        Birthdate = c.DateTime(nullable: false),
                        Sex = c.Int(nullable: false),
                        Address = c.String(),
                        MobileNumber = c.String(),
                        PhoneNumber = c.String(),
                        ExpectedSalary = c.Single(nullable: false),
                        Name = c.String(),
                        Notes = c.String(),
                        LastUpdated = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.JobApplications", "WithdrawnDate", c => c.DateTime());
            CreateIndex("dbo.JobApplications", "ApplicantId");
            AddForeignKey("dbo.JobApplications", "ApplicantId", "dbo.Applicants", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobApplications", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.JobApplications", new[] { "ApplicantId" });
            DropColumn("dbo.JobApplications", "WithdrawnDate");
            DropTable("dbo.Applicants");
        }
    }
}
