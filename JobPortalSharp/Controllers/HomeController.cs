using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using JobPortalSharp.Data;
using JobPortalSharp.Models;

namespace JobPortalSharp.Controllers
{
    public class HomeController : Controller
    {
        public JobPortalSharpDbContext db = new JobPortalSharpDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(string q, string l1, string l2)
        {
            var model = new SearchViewModel
            {
                search = q,
                Posts = db.JobPosts.Include(p => p.Employer)
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