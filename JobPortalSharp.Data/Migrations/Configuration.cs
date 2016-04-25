namespace JobPortalSharp.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<JobPortalSharp.Data.JobPortalSharpDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(JobPortalSharp.Data.JobPortalSharpDbContext context)
        {
            var lorem = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.";
            context.Employers.AddOrUpdate(
                new Employer { Name = "IBM", Details = lorem },
                new Employer { Name = "Microsoft", Details = lorem },
                new Employer { Name = "Google", Details = lorem },
                new Employer { Name = "Facebook", Details = lorem },
                new Employer { Name = "Amazon", Details = lorem }
            );
            context.SaveChanges();
            foreach (var emp in context.Employers.ToList())
            {
                context.JobPosts.AddOrUpdate(
                    new JobPost { 
                        CreateDate = DateTime.Now, 
                        EmployerId = emp.Id, 
                        ExpirationDate = DateTime.Parse("2016-06-01"), 
                        LastUpdated = DateTime.Now,
                        LastUpdatedBy = "System",
                        Name = "Software Engineer",
                        Salary = 30000
                    },
                    new JobPost
                    {
                        CreateDate = DateTime.Now,
                        EmployerId = emp.Id,
                        ExpirationDate = DateTime.Parse("2016-06-01"),
                        LastUpdated = DateTime.Now,
                        LastUpdatedBy = "System",
                        Name = "Web Developer",
                        Salary = 25000
                    },
                    new JobPost
                    {
                        CreateDate = DateTime.Now,
                        EmployerId = emp.Id,
                        ExpirationDate = DateTime.Parse("2016-06-01"),
                        LastUpdated = DateTime.Now,
                        LastUpdatedBy = "System",
                        Name = "Network Engineer",
                        Salary = 30000
                    },
                    new JobPost
                    {
                        CreateDate = DateTime.Now,
                        EmployerId = emp.Id,
                        ExpirationDate = DateTime.Parse("2016-06-01"),
                        LastUpdated = DateTime.Now,
                        LastUpdatedBy = "System",
                        Name = "Project Manager",
                        Salary = 40000
                    },
                    new JobPost
                    {
                        CreateDate = DateTime.Now,
                        EmployerId = emp.Id,
                        ExpirationDate = DateTime.Parse("2016-06-01"),
                        LastUpdated = DateTime.Now,
                        LastUpdatedBy = "System",
                        Name = "Mobile Developer",
                        Salary = 30000
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
