using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using JobPortalSharp.Data.Entities;

namespace JobPortalSharp.Data
{
    public class JobPortalSharpDbContext : IdentityDbContext<ApplicationUser>
    {
        public JobPortalSharpDbContext() : base("JobPortalSharpDbContext") { }
        public JobPortalSharpDbContext(string connStr) : base(connStr) { }

        public static JobPortalSharpDbContext Create()
        {
            return new JobPortalSharpDbContext();
        }

        public DbSet<Employer> Employers { get; set; }
        public DbSet<JobPost> JobPosts { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<EmploymentType> EmploymentTypes { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Suburb> Suburbs { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<EmployerType> EmployerTypes { get; set; }
        public DbSet<JobSelection> JobSelections { get; set; }
    }
}
