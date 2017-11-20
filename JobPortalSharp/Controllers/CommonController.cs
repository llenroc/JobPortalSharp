using JobPortalSharp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobPortalSharp.Controllers
{
    public class CommonController : Controller
    {
        JobPortalSharpDbContext db = new JobPortalSharpDbContext();

        // GET: Common
        public ActionResult Index()
        {
            return View();
        }

        //todo: implement right approach
        public ActionResult WebsiteTitle()
        {
            var websiteTitle = db.Settings.Single(x => x.Name == "application.title").Value;
            return PartialView("_WebsiteTitle", websiteTitle);
        } 
    }
}