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
                        Name = c.String(),
                        Notes = c.String(),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        LastUpdatedById = c.String(maxLength: 128),
                        LastUpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById)
                .ForeignKey("dbo.AspNetUsers", t => t.LastUpdatedById)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastUpdatedById);
            
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
                        CompanyName = c.Int(nullable: false),
                        CompanyAddress1 = c.String(),
                        CompanyAddress2 = c.String(),
                        NumberOfEmployees = c.Int(nullable: false),
                        CompanyLogoFileName = c.String(),
                        CompanyLogoSystemFileName = c.String(),
                        Name = c.String(),
                        Notes = c.String(),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        LastUpdatedById = c.String(maxLength: 128),
                        LastUpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById)
                .ForeignKey("dbo.AspNetUsers", t => t.LastUpdatedById)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastUpdatedById);
            
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
                        Notes = c.String(),
                        CreatedById = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(),
                        LastUpdatedById = c.String(maxLength: 128),
                        LastUpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById)
                .ForeignKey("dbo.Employers", t => t.EmployerId, cascadeDelete: true)
                .ForeignKey("dbo.EmploymentTypes", t => t.EmploymentTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Industries", t => t.IndustryId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.LastUpdatedById)
                .Index(t => t.EmployerId)
                .Index(t => t.EmploymentTypeId)
                .Index(t => t.IndustryId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastUpdatedById);
            
            CreateTable(
                "dbo.JobApplications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobPostId = c.Int(nullable: false),
                        ApplicationDate = c.DateTime(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EmailAddress = c.String(),
                        CvFileName = c.String(),
                        CvSystemFileName = c.String(),
                        CoverLetterFileName = c.String(),
                        CoverLetterSystemFileName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JobPosts", t => t.JobPostId, cascadeDelete: true)
                .Index(t => t.JobPostId);
            
            CreateTable(
                "dbo.EmploymentTypes",
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
            
            CreateTable(
                "dbo.Industries",
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
            
            CreateTable(
                "dbo.EmployerTypes",
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
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShortName = c.String(),
                        Name = c.String(),
                        Lattitude = c.Double(),
                        Longitude = c.Double(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Suburbs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StateId = c.Int(),
                        Name = c.String(),
                        Lattitude = c.Double(),
                        Longitude = c.Double(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.States", t => t.StateId)
                .Index(t => t.StateId);
            InitializeDb();
        }

        private void InitializeDb()
        {
            Sql("INSERT INTO EmployerTypes(Name) VALUES ('Direct Employer')");
            Sql("INSERT INTO EmployerTypes(Name) VALUES ('Agency')");

            Sql("INSERT INTO EmploymentTypes(Name) VALUES ('Permanent')");
            Sql("INSERT INTO EmploymentTypes(Name) VALUES ('Part Time')");

            Sql("INSERT INTO Industries(Name) VALUES('Agriculture')");
            Sql("INSERT INTO Industries(Name) VALUES('Construction')");
            Sql("INSERT INTO Industries(Name) VALUES('Mining')");
            Sql("INSERT INTO Industries(Name) VALUES('IT')");
            Sql("INSERT INTO Industries(Name) VALUES('Retail')");

            Sql("INSERT INTO States(Name) VALUES('South Australia')");
            Sql("INSERT INTO States(Name) VALUES('New South Wales')");
            Sql("INSERT INTO States(Name) VALUES('Tasmania')");
            Sql("INSERT INTO States(Name) VALUES('Queensland')");
            Sql("INSERT INTO States(Name) VALUES('Victoria')");
            Sql("INSERT INTO States(Name) VALUES('Western Australia')");
            Sql("INSERT INTO States(Name) VALUES('Australian Capital Territory')");
            Sql("INSERT INTO States(Name) VALUES('Northern Territory')");

            Sql("INSERT INTO Suburbs(Name) VALUES('Suburb 1')");
            Sql("INSERT INTO Suburbs(Name) VALUES('Suburb 2')");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Suburbs", "StateId", "dbo.States");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.EmployerTypes", "LastUpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.EmployerTypes", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Employers", "LastUpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobPosts", "LastUpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobPosts", "IndustryId", "dbo.Industries");
            DropForeignKey("dbo.Industries", "LastUpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Industries", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobPosts", "EmploymentTypeId", "dbo.EmploymentTypes");
            DropForeignKey("dbo.EmploymentTypes", "LastUpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.EmploymentTypes", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobPosts", "EmployerId", "dbo.Employers");
            DropForeignKey("dbo.JobPosts", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobApplications", "JobPostId", "dbo.JobPosts");
            DropForeignKey("dbo.Employers", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Employers", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Applicants", "LastUpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Applicants", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Applicants", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Suburbs", new[] { "StateId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.EmployerTypes", new[] { "LastUpdatedById" });
            DropIndex("dbo.EmployerTypes", new[] { "CreatedById" });
            DropIndex("dbo.Industries", new[] { "LastUpdatedById" });
            DropIndex("dbo.Industries", new[] { "CreatedById" });
            DropIndex("dbo.EmploymentTypes", new[] { "LastUpdatedById" });
            DropIndex("dbo.EmploymentTypes", new[] { "CreatedById" });
            DropIndex("dbo.JobApplications", new[] { "JobPostId" });
            DropIndex("dbo.JobPosts", new[] { "LastUpdatedById" });
            DropIndex("dbo.JobPosts", new[] { "CreatedById" });
            DropIndex("dbo.JobPosts", new[] { "IndustryId" });
            DropIndex("dbo.JobPosts", new[] { "EmploymentTypeId" });
            DropIndex("dbo.JobPosts", new[] { "EmployerId" });
            DropIndex("dbo.Employers", new[] { "LastUpdatedById" });
            DropIndex("dbo.Employers", new[] { "CreatedById" });
            DropIndex("dbo.Employers", new[] { "ApplicationUserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Applicants", new[] { "LastUpdatedById" });
            DropIndex("dbo.Applicants", new[] { "CreatedById" });
            DropIndex("dbo.Applicants", new[] { "ApplicationUserId" });
            DropTable("dbo.Suburbs");
            DropTable("dbo.States");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.EmployerTypes");
            DropTable("dbo.Industries");
            DropTable("dbo.EmploymentTypes");
            DropTable("dbo.JobApplications");
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
