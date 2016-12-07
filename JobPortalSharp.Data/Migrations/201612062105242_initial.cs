namespace JobPortalSharp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applicants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        Birthdate = c.DateTime(nullable: false),
                        Sex = c.Int(nullable: false),
                        Address = c.String(),
                        MobileNumber = c.String(),
                        PhoneNumber = c.String(),
                        ExpectedSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedById)
                .ForeignKey("dbo.AspNetUsers", t => t.LastUpdatedById)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastUpdatedById)
                .Index(t => t.DeletedById);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Employers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        CompanyDescription = c.String(),
                        AddressStreet = c.String(),
                        AddressTown = c.String(),
                        AddressState = c.String(),
                        AddressCountry = c.String(),
                        AddressPostalCode = c.String(),
                        AddressLongitude = c.Double(),
                        AddressLatitude = c.Double(),
                        NumberOfEmployees = c.Int(nullable: false),
                        CompanyLogoFileName = c.String(),
                        CompanyLogoSystemFileName = c.String(),
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
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedById)
                .ForeignKey("dbo.AspNetUsers", t => t.LastUpdatedById)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastUpdatedById)
                .Index(t => t.DeletedById);
            
            CreateTable(
                "dbo.JobPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Details = c.String(),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalaryRangeFrom = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalaryRangeTo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PostDate = c.DateTime(),
                        ExpirationDate = c.DateTime(),
                        EmployerId = c.Int(nullable: false),
                        EmploymentTypeId = c.Int(nullable: false),
                        IndustryId = c.Int(nullable: false),
                        LocationSameAsEmployer = c.Boolean(nullable: false),
                        AddressStreet = c.String(),
                        AddressTown = c.String(),
                        AddressState = c.String(),
                        AddressCountry = c.String(),
                        AddressLongitude = c.Double(),
                        AddressLatitude = c.Double(),
                        Notes = c.String(),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        LastUpdatedById = c.String(maxLength: 128),
                        LastUpdatedDate = c.DateTime(),
                        DeletedById = c.String(maxLength: 128),
                        DeletedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedById)
                .ForeignKey("dbo.Employers", t => t.EmployerId, cascadeDelete: true)
                .ForeignKey("dbo.EmploymentTypes", t => t.EmploymentTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Industries", t => t.IndustryId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.LastUpdatedById)
                .Index(t => t.EmployerId)
                .Index(t => t.EmploymentTypeId)
                .Index(t => t.IndustryId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastUpdatedById)
                .Index(t => t.DeletedById);
            
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
            
            CreateTable(
                "dbo.JobApplicationHeaders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationDate = c.DateTime(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EmailAddress = c.String(),
                        CvFileName = c.String(),
                        CvSystemFileName = c.String(),
                        CoverLetterFileName = c.String(),
                        CoverLetterSystemFileName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmploymentTypes",
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
            
            CreateTable(
                "dbo.Industries",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CategoryId = c.Int(),
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
                .ForeignKey("dbo.IndustryCategories", t => t.CategoryId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedById)
                .ForeignKey("dbo.AspNetUsers", t => t.LastUpdatedById)
                .Index(t => t.CategoryId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastUpdatedById)
                .Index(t => t.DeletedById);
            
            CreateTable(
                "dbo.IndustryCategories",
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
            
            CreateTable(
                "dbo.EmployerTypes",
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
            
            CreateTable(
                "dbo.JobSelections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
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
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.JobSelections", "LastUpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobSelections", "DeletedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobSelections", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.EmployerTypes", "LastUpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.EmployerTypes", "DeletedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.EmployerTypes", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Employers", "LastUpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobPosts", "LastUpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobPosts", "IndustryId", "dbo.Industries");
            DropForeignKey("dbo.Industries", "LastUpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Industries", "DeletedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Industries", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.IndustryCategories", "LastUpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Industries", "CategoryId", "dbo.IndustryCategories");
            DropForeignKey("dbo.IndustryCategories", "DeletedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.IndustryCategories", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobPosts", "EmploymentTypeId", "dbo.EmploymentTypes");
            DropForeignKey("dbo.EmploymentTypes", "LastUpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.EmploymentTypes", "DeletedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.EmploymentTypes", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobPosts", "EmployerId", "dbo.Employers");
            DropForeignKey("dbo.JobPosts", "DeletedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobPosts", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobApplicationDetails", "JobPostId", "dbo.JobPosts");
            DropForeignKey("dbo.JobApplicationDetails", "JobApplicationHeaderId", "dbo.JobApplicationHeaders");
            DropForeignKey("dbo.Employers", "DeletedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Employers", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Employers", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Applicants", "LastUpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Applicants", "DeletedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Applicants", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Applicants", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.JobSelections", new[] { "DeletedById" });
            DropIndex("dbo.JobSelections", new[] { "LastUpdatedById" });
            DropIndex("dbo.JobSelections", new[] { "CreatedById" });
            DropIndex("dbo.EmployerTypes", new[] { "DeletedById" });
            DropIndex("dbo.EmployerTypes", new[] { "LastUpdatedById" });
            DropIndex("dbo.EmployerTypes", new[] { "CreatedById" });
            DropIndex("dbo.IndustryCategories", new[] { "DeletedById" });
            DropIndex("dbo.IndustryCategories", new[] { "LastUpdatedById" });
            DropIndex("dbo.IndustryCategories", new[] { "CreatedById" });
            DropIndex("dbo.Industries", new[] { "DeletedById" });
            DropIndex("dbo.Industries", new[] { "LastUpdatedById" });
            DropIndex("dbo.Industries", new[] { "CreatedById" });
            DropIndex("dbo.Industries", new[] { "CategoryId" });
            DropIndex("dbo.EmploymentTypes", new[] { "DeletedById" });
            DropIndex("dbo.EmploymentTypes", new[] { "LastUpdatedById" });
            DropIndex("dbo.EmploymentTypes", new[] { "CreatedById" });
            DropIndex("dbo.JobApplicationDetails", new[] { "JobApplicationHeaderId" });
            DropIndex("dbo.JobApplicationDetails", new[] { "JobPostId" });
            DropIndex("dbo.JobPosts", new[] { "DeletedById" });
            DropIndex("dbo.JobPosts", new[] { "LastUpdatedById" });
            DropIndex("dbo.JobPosts", new[] { "CreatedById" });
            DropIndex("dbo.JobPosts", new[] { "IndustryId" });
            DropIndex("dbo.JobPosts", new[] { "EmploymentTypeId" });
            DropIndex("dbo.JobPosts", new[] { "EmployerId" });
            DropIndex("dbo.Employers", new[] { "DeletedById" });
            DropIndex("dbo.Employers", new[] { "LastUpdatedById" });
            DropIndex("dbo.Employers", new[] { "CreatedById" });
            DropIndex("dbo.Employers", new[] { "ApplicationUserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Applicants", new[] { "DeletedById" });
            DropIndex("dbo.Applicants", new[] { "LastUpdatedById" });
            DropIndex("dbo.Applicants", new[] { "CreatedById" });
            DropIndex("dbo.Applicants", new[] { "ApplicationUserId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.JobSelections");
            DropTable("dbo.EmployerTypes");
            DropTable("dbo.IndustryCategories");
            DropTable("dbo.Industries");
            DropTable("dbo.EmploymentTypes");
            DropTable("dbo.JobApplicationHeaders");
            DropTable("dbo.JobApplicationDetails");
            DropTable("dbo.JobPosts");
            DropTable("dbo.Employers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Applicants");
        }
    }
}
