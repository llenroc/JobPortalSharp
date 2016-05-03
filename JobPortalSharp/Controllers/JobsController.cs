using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using JobPortalSharp.Data;
using Microsoft.AspNet.Identity;

namespace JobPortalSharp.Controllers
{
    public class JobsController : Controller
    {
        public JobPortalSharpDbContext db = new JobPortalSharpDbContext();

        [HttpGet]
        public ActionResult Apply(int id)
        {
            var job = db.JobPosts.Include(x => x.Employer).Single(x => x.Id == id);

            return View(job);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Apply(JobPost job)
        {
            var applicant = db.Applicants.ToList().Single(x => x.SystemId == User.Identity.GetUserId());
            db.Applications.Add(new JobApplication { ApplicantId = applicant.Id, ApplicationDate = DateTime.Now, JobPostId = job.Id });
            db.SaveChanges();

            return View("ApplySuccess");
        }
    }
}