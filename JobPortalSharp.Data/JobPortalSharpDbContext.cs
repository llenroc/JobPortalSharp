using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using JobPortalSharp.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

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
        public DbSet<JobApplicationHeader> JobApplicationHeaders { get; set; }
        public DbSet<JobApplicationDetail> JobApplicationDetails { get; set; }
        public DbSet<EmployerType> EmployerTypes { get; set; }
        public DbSet<JobSelection> JobSelections { get; set; }
        public DbSet<IndustryCategory> IndustryCategories { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IndustryCategory>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<EmployerType>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<EmploymentType>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Industry>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<IndustryCategory>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Country>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
