using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using JobPortalSharp.Data;
using JobPortalSharp.Models;
using X.PagedList;
using JobPortalSharp.Data.Dto;
using Microsoft.AspNet.Identity;

namespace JobPortalSharp.Controllers
{
    public class HomeController : Controller
    {
        public JobPortalSharpDbContext db = new JobPortalSharpDbContext();

        public ActionResult Index()
        {
            ViewBag.EmployerId = new SelectList(db.Employers, "Id", "ApplicationUserId");
            ViewBag.Industries = db.Industries.ToList();

            var model = new SearchViewModel();

            model.EmployerTypes = db.EmployerTypes.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            model.EmploymentTypes = db.EmploymentTypes.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            return View(model);
        }

        public ActionResult Search(SearchViewModel model)
        {
            var pageNumber = model.p ?? 1;
            var pageSize = model.p ?? 10;
            var query = db.JobPosts.Include(x => x.Employer);
            if (string.IsNullOrWhiteSpace(model.q) == false)
            {
                query = query.Where(x => x.Name.Contains(model.q) || x.Employer.Name.Contains(model.q));
            }

            model.ResultCount = query.Count();
            model.Posts = query.OrderByDescending(x => x.PostDate).Select(x => new JobPostDto
            {
                Details = x.Details,
                EmployerId = x.EmployerId,
                EmployerName = x.Employer.Name,
                EmploymentTypeName = x.EmploymentType.Name,
                IndustryName = x.Industry.Name,
                Id = x.Id,
                Name = x.Name,
                Salary = x.Salary,
                SalaryRangeFrom = x.SalaryRangeFrom,
                SalaryRangeTo = x.SalaryRangeTo
            }).ToPagedList(pageNumber, pageSize);

            

            //todo: get user's location
            //todo: search jobs based on user's location sorted by distance

            model.EmployerTypes = db.EmployerTypes.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            model.EmploymentTypes = db.EmploymentTypes.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            var userId = User.Identity.GetUserId();
            ViewBag.JobSelectionCount = db.JobSelections.Where(x => x.CreatedById == userId).Count();
            
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult RecentJobs()
        {
            var jobs = db.JobPosts.Include(j => j.EmploymentType).OrderByDescending(j => j.PostDate);
            return PartialView(jobs);
        }
    }
}