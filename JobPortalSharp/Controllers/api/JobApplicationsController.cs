using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using JobPortalSharp.Data;
using Microsoft.AspNet.Identity;

namespace JobPortalSharp.Controllers.api
{
    public class JobApplicationsController : ApiController
    {
        private JobPortalSharpDbContext db = new JobPortalSharpDbContext();

        // GET: api/JobApplications
        public IQueryable<JobApplication2> GetApplications()
        {
            var userId = User.Identity.GetUserId();
            var employerId = db.Employers.Single(x => x.ApplicationUserId == userId).Id;
            return db.JobApplications.Where(x => x.JobPost.EmployerId == employerId);
        }
    }
}