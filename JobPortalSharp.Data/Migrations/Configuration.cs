namespace JobPortalSharp.Data.Migrations
{
    using Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web;
    using System.IO;
    using System.Diagnostics;

    public sealed class Configuration : DbMigrationsConfiguration<JobPortalSharpDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true; //todo: remove this
            AutomaticMigrationDataLossAllowed = true; //todo: remove this
            
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

            if (context.States.Count() == 0)
            {
                context.States.AddOrUpdate(
                    new State { Name = "New South Wales", ShortName = "NSW" },
                    new State { Name = "Victoria", ShortName = "VIC" },
                    new State { Name = "Queensland", ShortName = "QLD" },
                    new State { Name = "South Australia", ShortName = "SA" },
                    new State { Name = "Northern Territory", ShortName = "NT" },
                    new State { Name = "Australian Capital Territory", ShortName = "ACT" },
                    new State { Name = "Western Australia", ShortName = "WA" },
                    new State { Name = "Tasmania", ShortName = "TAS" },
                    new State { Name = "Other Territories", ShortName = "OT" });
                context.SaveChanges();
            }

            var states = context.States.ToList();

            if (context.Suburbs.Count() == 0)
            {
                string path = AppDomain.CurrentDomain.GetData("DataDirectory").ToString() + "\\Australian.Suburbs.and.Geocodes.csv";

                var fastdb = new JobPortalSharpDbContext();
                fastdb.Configuration.AutoDetectChangesEnabled = false;
                var file = new StreamReader(path);
                string line = file.ReadLine();

                int count = 0;

                while (line != null)
                {
                    var values = line.Split(',');
                    var state = states.SingleOrDefault(x => x.ShortName.Equals(values[1].Trim(), StringComparison.OrdinalIgnoreCase));

                    Double longitude, latitude;

                    if (state != null && Double.TryParse(values[3], out longitude) && Double.TryParse(values[4], out latitude))
                    {
                        count++;
                        fastdb.Suburbs.Add(new Suburb
                        {
                            Name = values[0],
                            StateId = state.Id,
                            Longitude = longitude,
                            Lattitude = latitude
                        });

                        if (count % 100 == 0)
                        {
                            fastdb.SaveChanges();
                            fastdb.Dispose();
                            fastdb = new JobPortalSharpDbContext();
                            fastdb.Configuration.AutoDetectChangesEnabled = false;
                        }
                    }
                    else
                    {
                        Debug.WriteLine(line);
                    }
                    line = file.ReadLine();
                }
                if (count % 100 != 0)
                {
                    fastdb.SaveChanges();
                }
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
