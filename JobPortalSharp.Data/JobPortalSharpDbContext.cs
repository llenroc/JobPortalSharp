using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

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

        public DbSet<JobApplication> Applications { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<JobPost> JobPosts { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
    }
}
