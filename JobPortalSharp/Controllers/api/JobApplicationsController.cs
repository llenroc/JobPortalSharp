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
    public class JobApplicationsController : ApiControllerBase
    {
        // GET: api/JobApplications
        public IQueryable<JobApplicationHeader> GetApplications(int id)
        {
            var userId = User.Identity.GetUserId();
            var employerId = db.Employers.Single(x => x.ApplicationUserId == userId).Id;
            return db.JobApplicationDetails.Where(x => x.JobPostId == id).Select(x => x.JobApplicationHeader);
        }
    }
}