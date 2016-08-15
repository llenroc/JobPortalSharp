namespace JobPortalSharp.Data.Migrations
{
    using Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<JobPortalSharpDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(JobPortalSharp.Data.JobPortalSharpDbContext context)
        {
            var lorem = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.";

            if (context.Roles.Count() == 0)
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);

                var role = new IdentityRole { Name = "Employer" };
                manager.Create(role);

                role = new IdentityRole { Name = "Applicant" };
                manager.Create(role);
            }

            if (context.Industries.Count() == 0)
            {
                context.Industries.AddOrUpdate(
                new Industry { Name = "Information Technology" },
                new Industry { Name = "Manufacturing" },
                new Industry { Name = "Health Care" },
                new Industry { Name = "Finance" },
                new Industry { Name = "Retail" },
                new Industry { Name = "Accounting and Legal" },
                new Industry { Name = "Construction, repair and maintenance" });
                context.SaveChanges();
            }

            if (context.EmploymentTypes.Count() == 0)
            {
                context.EmploymentTypes.AddOrUpdate(
                new EmploymentType { Name = "Full-time" },
                new EmploymentType { Name = "Part-time" },
                new EmploymentType { Name = "Contract" },
                new EmploymentType { Name = "Internship" },
                new EmploymentType { Name = "Retail" });
                context.SaveChanges();
            }

            if (context.Employers.Count() == 0)
            {
                context.Employers.AddOrUpdate(
                new Employer { Name = "IBM", CompanyDescription = lorem },
                new Employer { Name = "Microsoft", CompanyDescription = lorem },
                new Employer { Name = "Google", CompanyDescription = lorem },
                new Employer { Name = "Facebook", CompanyDescription = lorem },
                new Employer { Name = "Amazon", CompanyDescription = lorem });
                context.SaveChanges();

                var industryId = context.Industries.FirstOrDefault(x => x.Name == "Information Technology").Id;
                var empTypeId = context.EmploymentTypes.FirstOrDefault(x => x.Name == "Full-time").Id;
                foreach (var emp in context.Employers.ToList())
                {
                    context.JobPosts.AddOrUpdate(
                        new JobPost
                        {
                            PostDate = DateTime.Now,
                            EmployerId = emp.Id,
                            ExpirationDate = DateTime.Now.AddDays(30),
                            LastUpdatedDate = DateTime.Now,
                            LastUpdatedById = "System",
                            Name = "Software Engineer",
                            Salary = 30000,
                            EmploymentTypeId = empTypeId,
                            IndustryId = industryId
                        },
                        new JobPost
                        {
                            PostDate = DateTime.Now,
                            EmployerId = emp.Id,
                            ExpirationDate = DateTime.Now.AddDays(30),
                            LastUpdatedDate = DateTime.Now,
                            LastUpdatedById = "System",
                            Name = "Web Developer",
                            Salary = 25000,
                            EmploymentTypeId = empTypeId,
                            IndustryId = industryId
                        },
                        new JobPost
                        {
                            PostDate = DateTime.Now,
                            EmployerId = emp.Id,
                            ExpirationDate = DateTime.Now.AddDays(30),
                            LastUpdatedDate = DateTime.Now,
                            LastUpdatedById = "System",
                            Name = "Network Engineer",
                            Salary = 30000,
                            EmploymentTypeId = empTypeId,
                            IndustryId = industryId
                        },
                        new JobPost
                        {
                            PostDate = DateTime.Now,
                            EmployerId = emp.Id,
                            ExpirationDate = DateTime.Parse("2016-06-01"),
                            LastUpdatedDate = DateTime.Now,
                            LastUpdatedById = "System",
                            Name = "Project Manager",
                            Salary = 40000,
                            EmploymentTypeId = empTypeId,
                            IndustryId = industryId
                        },
                        new JobPost
                        {
                            PostDate = DateTime.Now,
                            EmployerId = emp.Id,
                            ExpirationDate = DateTime.Parse("2016-06-01"),
                            LastUpdatedDate = DateTime.Now,
                            LastUpdatedById = "System",
                            Name = "Mobile Developer",
                            Salary = 30000,
                            EmploymentTypeId = empTypeId,
                            IndustryId = industryId
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
