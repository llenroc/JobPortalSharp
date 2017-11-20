using JobPortalSharp.Data;
using JobPortalSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobPortalSharp.Controllers
{
    public class AdminController : Controller
    {
        JobPortalSharpDbContext db = new JobPortalSharpDbContext();

        // GET: Admin
        public ActionResult Index()
        {
            var model = new SettingsViewModel();
            model.WebsiteTitle = db.Settings.Single(x => x.Name == "application.title").Value;
            model.FooterText = db.Settings.Single(x => x.Name == "application.footer").Value;
            model.HomePageWelcomeMessage = db.Settings.Single(x => x.Name == "homepage.welcomemessage").Value;
            model.HomePageWelcomeMessageSubtext = db.Settings.Single(x => x.Name == "homepage.welcomemessage.subtext").Value;
            model.HomePageBottomText = db.Settings.Single(x => x.Name == "homepage.bottomtext").Value;
            model.AboutText = db.Settings.Single(x => x.Name == "about.text").Value;

            return View(model);
        }

        public ActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveSettings(SettingsViewModel model)
        {
            db.Settings.Single(x => x.Name == "application.title").Value = model.WebsiteTitle;
            db.Settings.Single(x => x.Name == "application.footer").Value = model.FooterText;
            db.Settings.Single(x => x.Name == "homepage.welcomemessage").Value = model.HomePageWelcomeMessage;
            db.Settings.Single(x => x.Name == "homepage.welcomemessage.subtext").Value = model.HomePageWelcomeMessageSubtext;
            db.Settings.Single(x => x.Name == "homepage.bottomtext");
            db.Settings.Single(x => x.Name == "about.text").Value = model.AboutText;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}