﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using JobPortalSharp.Data;
using JobPortalSharp.Models;
using JobPortalSharp.Data.Dto;
using Microsoft.AspNet.Identity;

namespace JobPortalSharp.Controllers
{
    class JobGeolocation
    {
        public int Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }

    public class HomeController : Controller
    {
        public JobPortalSharpDbContext db = new JobPortalSharpDbContext();

        public ActionResult Index()
        {
            ViewBag.EmployerId = new SelectList(db.Employers, "Id", "ApplicationUserId");
            ViewBag.Industries = db.Industries.ToList();

            var model = new HomeViewModel();

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

            model.WebsiteTitle = db.Settings.Single(x => x.Name == "application.title").Value;
            model.FooterText = db.Settings.Single(x => x.Name == "application.footer").Value;
            model.HomePageWelcomeMessage = db.Settings.Single(x => x.Name == "homepage.welcomemessage").Value;
            model.HomePageWelcomeMessageSubtext = db.Settings.Single(x => x.Name == "homepage.welcomemessage.subtext").Value;
            model.HomePageBottomText = db.Settings.Single(x => x.Name == "homepage.bottomtext").Value;
            model.AboutText = db.Settings.Single(x => x.Name == "about.text").Value;

            return View(model);
        }

        public ActionResult Index2()
        {
            ViewBag.EmployerId = new SelectList(db.Employers, "Id", "ApplicationUserId");
            ViewBag.Industries = db.Industries.ToList();

            var model = new HomeViewModel();

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