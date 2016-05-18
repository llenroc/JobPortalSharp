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
        public ActionResult Apply(JobPost job)
        {
            return View("ApplySuccess");
        }
    }
}