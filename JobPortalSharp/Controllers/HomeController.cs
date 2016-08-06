using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using JobPortalSharp.Data;
using JobPortalSharp.Models;
using X.PagedList;

namespace JobPortalSharp.Controllers
{
    public class HomeController : Controller
    {
        public JobPortalSharpDbContext db = new JobPortalSharpDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(string q, string l1, string l2, int? page)
        {
            var pageNumber = page ?? 1;

            var model = new SearchViewModel
            {
                q = q,
                l1 = l1,
                l2 = l2,
                Posts = db.JobPosts.Include(p => p.Employer).ToPagedList(pageNumber, 25)
            };

            //todo: get user's location
            //todo: search jobs based on user's location sorted by distance
            
            
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
    }
}