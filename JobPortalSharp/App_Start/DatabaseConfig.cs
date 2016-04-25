using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using JobPortalSharp.Data;
using JobPortalSharp.Models;

namespace JobPortalSharp
{
    public class DatabaseConfig
    {
        public static void RegisterDatabase()
        {
            Database.SetInitializer<JobPortalSharpDbContext>(new MigrateDatabaseToLatestVersion<JobPortalSharpDbContext, JobPortalSharp.Data.Migrations.Configuration>());
            Database.SetInitializer<JobPortalSharpIdentityDbContext>(new MigrateDatabaseToLatestVersion<JobPortalSharpIdentityDbContext, JobPortalSharp.Migrations.Configuration>());
        }
    }
}