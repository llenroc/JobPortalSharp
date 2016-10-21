using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using JobPortalSharp.Data;
using Microsoft.AspNet.Identity;
using JobPortalSharp.Models;
using JobPortalSharp.Data.Dto;
using X.PagedList;

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

        public ActionResult Category(int id, SearchViewModel model)
        {
            var pageNumber = model.p ?? 1;
            var pageSize = model.p ?? 10;
            var posts = db.JobPosts
                .Include(x => x.Employer)
                .Include(x => x.EmploymentType)
                .Include(x => x.Industry)
                .Where(x => x.IndustryId == id);

            model.ResultCount = posts.Count();
            model.Posts = posts.Select(x => new JobPostDto
            {
                Details = x.Details,
                EmployerId = x.EmployerId,
                EmployerName = x.Employer.Name,
                EmploymentTypeName = x.EmploymentType.Name,
                ExpirationDate = x.ExpirationDate,
                Id = x.Id,
                IndustryName = x.Industry.Name,
                Name = x.Name,
                Salary = x.Salary,
                SalaryRangeFrom = x.SalaryRangeFrom,
                SalaryRangeTo = x.SalaryRangeTo
            }).ToPagedList(pageNumber, pageSize);
            return View(model);
        }
    }
}