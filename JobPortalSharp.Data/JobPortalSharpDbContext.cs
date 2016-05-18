using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalSharp.Data
{
    public class JobPortalSharpDbContext : DbContext
    {
        public JobPortalSharpDbContext() : base("JobPortalSharpDbContext") { }
        public JobPortalSharpDbContext(string connStr) : base(connStr) { }

        public DbSet<JobApplication> Applications { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<JobPost> JobPosts { get; set; }
    }
}
