using JobPortalSharp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using JobPortalSharp.Models;

namespace JobPortalSharp.Controllers
{
    [Authorize(Roles = "Employer")]
    public class PostsController : Controller
    {
        public JobPortalSharpDbContext db = new JobPortalSharpDbContext();

        // GET: Posts
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var list = db.JobPosts.Where(x => x.Employer.ApplicationUserId == userId);

            return View(list);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var model = new JobPostViewModel();
            model.EmploymentTypes = db.EmploymentTypes.ToList();
            model.Industries = db.Industries.ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(JobPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var employerId = db.Employers.Single(x => x.ApplicationUserId == userId).Id;
                db.JobPosts.Add(new JobPost
                {
                    PostDate = DateTime.Now,
                    Details = model.Details,
                    EmployerId = employerId,
                    EmploymentTypeId = model.EmploymentTypeId,
                    ExpirationDate = model.ExpirationDate,
                    IndustryId = model.IndustryId,
                    Name = model.Name,
                    Salary = model.Salary,
                    SalaryRangeFrom = model.SalaryRangeFrom,
                    SalaryRangeTo = model.SalaryRangeTo
                });
                db.SaveChanges();
                return RedirectToAction("Index", "JobPosts");
            }
            return View(model);
        }
    }
}