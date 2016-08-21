using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobPortalSharp.Data;
using Microsoft.AspNet.Identity;
using JobPortalSharp.Services;
using JobPortalSharp.Models;
using System.IO;

namespace JobPortalSharp.Controllers
{
    public class JobPostsController : Controller
    {
        private JobPortalSharpDbContext db = new JobPortalSharpDbContext();

        // GET: JobPosts
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var jobPosts = db.JobPosts.Include(j => j.Employer).Include(j => j.EmploymentType).Include(j => j.Industry).Where(j => j.Employer.ApplicationUserId == userId);
            return View(jobPosts.ToList());
        }

        // GET: JobPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPost jobPost = db.JobPosts.Include(X => X.EmploymentType).Include(x => x.Industry).SingleOrDefault(x => x.Id == id);
            if (jobPost == null)
            {
                return HttpNotFound();
            }
            return View(jobPost);
        }

        // GET: JobPosts/Create
        public ActionResult Create()
        {
            ViewBag.EmployerId = new SelectList(db.Employers, "Id", "ApplicationUserId");
            ViewBag.EmploymentTypeId = new SelectList(db.EmploymentTypes, "Id", "Name");
            ViewBag.IndustryId = new SelectList(db.Industries, "Id", "Name");
            return View();
        }

        // POST: JobPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JobPost jobPost)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();

                jobPost.EmployerId = db.Employers.Single(x => x.ApplicationUserId == userId).Id;
                jobPost.CreatedDate = DateTime.Now;
                jobPost.CreatedById = userId;
                db.JobPosts.Add(jobPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployerId = new SelectList(db.Employers, "Id", "ApplicationUserId", jobPost.EmployerId);
            ViewBag.EmploymentTypeId = new SelectList(db.EmploymentTypes, "Id", "Name", jobPost.EmploymentTypeId);
            ViewBag.IndustryId = new SelectList(db.Industries, "Id", "Name", jobPost.IndustryId);
            return View(jobPost);
        }

        // GET: JobPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPost jobPost = db.JobPosts.Find(id);
            if (jobPost == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployerId = new SelectList(db.Employers, "Id", "ApplicationUserId", jobPost.EmployerId);
            ViewBag.EmploymentTypeId = new SelectList(db.EmploymentTypes, "Id", "Name", jobPost.EmploymentTypeId);
            ViewBag.IndustryId = new SelectList(db.Industries, "Id", "Name", jobPost.IndustryId);
            return View(jobPost);
        }

        // POST: JobPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(JobPost jobPost)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var old = db.JobPosts.Single(x => x.Id == jobPost.Id);
                db.Entry(old).State = EntityState.Detached;

                jobPost.EmployerId = old.EmployerId;
                jobPost.CreatedById = old.CreatedById;
                jobPost.CreatedDate = old.CreatedDate;
                jobPost.LastUpdatedDate = DateTime.Now;
                jobPost.LastUpdatedById = userId;
                db.Entry(jobPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployerId = new SelectList(db.Employers, "Id", "ApplicationUserId", jobPost.EmployerId);
            ViewBag.EmploymentTypeId = new SelectList(db.EmploymentTypes, "Id", "Name", jobPost.EmploymentTypeId);
            ViewBag.IndustryId = new SelectList(db.Industries, "Id", "Name", jobPost.IndustryId);
            return View(jobPost);
        }

        // GET: JobPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPost jobPost = db.JobPosts.Include(X => X.EmploymentType).Include(x => x.Industry).SingleOrDefault(x => x.Id == id);
            if (jobPost == null)
            {
                return HttpNotFound();
            }
            return View(jobPost);
        }

        // POST: JobPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobPost jobPost = db.JobPosts.Find(id);
            db.JobPosts.Remove(jobPost);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult Apply(int id, [System.Web.Http.FromBody] ApplyJobPost model)
        {
            var obj = new JobApplication2();
            obj.JobPostId = id;

            var appDataPath = Server.MapPath("~/App_Data");
            if (model.CV != null)
            {
                var cvSystemName = Guid.NewGuid().ToString() + ".dat";
                model.CV.SaveAs(appDataPath + "/" + cvSystemName);
                obj.CvSystemFileName = cvSystemName;
                obj.CvFileName = Path.GetFileName(model.CV.FileName);
            }

            if (model.CoverLetter != null)
            {
                var coverLetterSystemFileName = Guid.NewGuid().ToString() + ".dat";
                model.CoverLetter.SaveAs(appDataPath + "/" + coverLetterSystemFileName);
                obj.CoverLetterSystemFileName = coverLetterSystemFileName;
                obj.CoverLetterFileName = Path.GetFileName(model.CoverLetter.FileName);
            }

            db.JobApplications.Add(obj);
            db.SaveChanges();
            return new EmptyResult();
        }
    }
}
