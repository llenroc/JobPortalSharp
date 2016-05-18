namespace JobPortalSharp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveApplicantModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.JobApplications", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.JobApplications", new[] { "ApplicantId" });
            DropTable("dbo.Applicants");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Applicants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MiddleName = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        PhoneNumber = c.String(),
                        SystemId = c.String(),
                        Name = c.String(),
                        Notes = c.String(),
                        LastUpdated = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.JobApplications", "ApplicantId");
            AddForeignKey("dbo.JobApplications", "ApplicantId", "dbo.Applicants", "Id", cascadeDelete: true);
        }
    }
}
