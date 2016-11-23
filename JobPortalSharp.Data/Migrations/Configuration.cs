namespace JobPortalSharp.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;

    public sealed class Configuration : DbMigrationsConfiguration<JobPortalSharp.Data.JobPortalSharpDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(JobPortalSharp.Data.JobPortalSharpDbContext context)
        {
            string[] roles = new string[] { "Employer", "Applicant" };

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(context);

                if (!context.Roles.Any(r => r.Name == role))
                {
                    roleStore.CreateAsync(new IdentityRole(role));
                }
            }

            var rnd = new Random();

            if (context.Employers.Count() == 0)
            {
                var list = new List<Employer>();

                for (int i = 0; i < 100; i++)
                {
                    var numOfEmployees = rnd.Next(0, 5);
                    context.Employers.Add(
                        new Employer { 
                            Name = "company " + (i + 1).ToString(), 
                            NumberOfEmployees = (NumberOfEmployees)numOfEmployees
                        });
                }
                context.SaveChanges();
            }

            if (context.JobPosts.Count() == 0)
            {
                var details = "The quick brown fox jumps over the lazy dog";
                var industryCount = context.Industries.Count();
                var employerTypeCount = context.EmployerTypes.Count();
                var employerCount = context.Employers.Count();
                var list = new List<JobPost>();

                for (int i = 0; i < 1000; i++)
                {
                    var industryId = rnd.Next(1, industryCount);
                    context.JobPosts.Add(
                        new JobPost { 
                            Name = "job " + (i + 1).ToString(), 
                            Details = details, 
                            IndustryId = rnd.Next(1, industryCount), 
                            EmployerId = rnd.Next(1, employerCount),
                            EmploymentTypeId = rnd.Next(1, employerTypeCount)
                        });
                }

                context.SaveChanges();                
            }
        }
    }
}
