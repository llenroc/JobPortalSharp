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
        public IQueryable<JobApplicationHeader> GetApplications()
        {
            var userId = User.Identity.GetUserId();
            var employerId = db.Employers.Single(x => x.ApplicationUserId == userId).Id;
            return db.JobApplicationHeaders.Where(x => x.JobPost.EmployerId == employerId);
        }

        [Route("api/jobcart/add/{id}")]
        public IHttpActionResult AddToCart(int id)
        {
            db.JobSelections.Add(new Data.Entities.JobSelection
            {
                CreatedById = RequestContext.Principal.Identity.GetUserId(),
                CreatedDate = DateTime.Now,
                Name = id.ToString()
            });
            db.SaveChanges();
            return Ok();
        }

        [Route("api/jobcart/remove/{id}")]
        public IHttpActionResult RemoveFromCart(int id)
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            var strId = id.ToString();
            db.JobSelections.Where(x => x.CreatedById == userId && x.Name == strId).ToList().ForEach(x => db.JobSelections.Remove(x));
            db.SaveChanges();
            return Ok();
        }
    }
}