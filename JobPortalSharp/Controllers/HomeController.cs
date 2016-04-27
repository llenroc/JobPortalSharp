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

        public ActionResult Index(SearchViewModel model)
        {
            if (model.search == null)
            {
                return View(model);
            }
            else
            {
                model.Posts = db.JobPosts.Include(x => x.Employer).Where(x => x.Name.Contains(model.search) || x.Details.Contains(model.search)).ToList();
                return View("Search", model);
            }
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