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
using JobPortalSharp.Data.Dto;
using RestSharp;
using RestSharp.Authenticators;
using System.Configuration;

namespace JobPortalSharp.Controllers
{
    [Authorize]
    public class JobPostsController : Controller
    {
        private JobPortalSharpDbContext db = new JobPortalSharpDbContext();

        // GET: JobPosts
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var jobPosts = db.JobPosts
                .Include(j => j.Employer)
                .Include(j => j.EmploymentType)
                .Include(j => j.Industry)
                .Include(j => j.Applications)
                .Where(j => j.Employer.ApplicationUserId == userId)
                .ToList()
                .Select(j => new JobPostDto
                {
                    Details = j.Details,
                    EmployerName = j.Employer.Name,
                    EmploymentTypeName = j.EmploymentType.Name,
                    ExpirationDate = j.ExpirationDate,
                    Id = j.Id,
                    IndustryName = j.Industry.Name,
                    Name = j.Name,
                    NumOfApplications = j.Applications.Count,
                    Salary = j.Salary,
                    SalaryRangeFrom = j.SalaryRangeFrom,
                    SalaryRangeTo = j.SalaryRangeTo
                });
            return View(jobPosts.ToList());
        }

        [Route("jobpost/{id:int}")]
        public ActionResult ViewJobPost(int id)
        {
            return View();
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

            var model = new JobPost() { LocationSameAsEmployer = true };

            return View(model);
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

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Apply(int id, [System.Web.Http.FromBody] ApplyJobPost model)
        {
            var header = new JobApplicationHeader();
            header.ApplicationDate = DateTime.Now;
            header.EmailAddress = model.EmailAddress;
            header.Details = new List<JobApplicationDetail>();
            header.Details.Add(new JobApplicationDetail
            {
                JobPostId = id
            });

            SaveApplicationFiles(model, header);

            db.JobApplicationHeaders.Add(header);
            db.SaveChanges();
            return new EmptyResult();
            //todo: mailgun
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ApplyMultiple(int[] ids, [System.Web.Http.FromBody] ApplyJobPost model)
        {
            var header = new JobApplicationHeader();
            header.ApplicationDate = DateTime.Now;
            header.FirstName = model.FirstName;
            header.LastName = model.LastName;
            header.EmailAddress = model.EmailAddress;
            header.Details = new List<JobApplicationDetail>();
            for (int i = 0; i < ids.Length; i++)
            {
                header.Details.Add(new JobApplicationDetail{
                    JobPostId = ids[i]
                });
            }

            SaveApplicationFiles(model, header);

            db.JobApplicationHeaders.Add(header);
            db.SaveChanges();

            foreach (var jobPostId in ids)
            {
                var jobPost = db.JobPosts.Include(x => x.Employer.ApplicationUser).Single(x => x.Id == jobPostId);
                var client = new RestClient();
                client.BaseUrl = new Uri("https://api.mailgun.net/v3");

                string path = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/mailgun.key");
                var apiKey = System.IO.File.ReadAllText(path);
                client.Authenticator = new HttpBasicAuthenticator("api", apiKey);
                RestRequest request = new RestRequest();
                request.AddParameter("domain", "jp.irdocs.net", ParameterType.UrlSegment);
                request.Resource = "{domain}/messages";
                request.AddParameter("from", "admin@jp.irdocs.net");

                if (jobPost.Employer.ApplicationUser == null) //system generated employer
                {
                    request.AddParameter("to", "rajeem_cariazo@yahoo.com");
                }
                else
                {
                    request.AddParameter("to", jobPost.Employer.ApplicationUser.Email);
                }
                request.AddParameter("subject", "Application for \"" + jobPost.Name + "\"");
                request.AddParameter("html", "<div>Someone has recently applied on this job:<p><strong>Programmer </strong><a href=" + Request.Url.Authority + "/jobposts" + ">View My Job Posts</a></p></div>");
                request.Method = Method.POST;
                client.Execute(request);
            }

            return new EmptyResult();
        }

        private void SaveApplicationFiles(ApplyJobPost model, JobApplicationHeader header)
        {
            var appDataPath = Server.MapPath("~/App_Data/application_files");
            if (model.CV != null)
            {
                var cvSystemName = Guid.NewGuid().ToString() + ".dat";
                model.CV.SaveAs(appDataPath + "/" + cvSystemName);
                header.CvSystemFileName = cvSystemName;
                header.CvFileName = Path.GetFileName(model.CV.FileName);
            }

            if (model.CoverLetter != null)
            {
                var coverLetterSystemFileName = Guid.NewGuid().ToString() + ".dat";
                model.CoverLetter.SaveAs(appDataPath + "/" + coverLetterSystemFileName);
                header.CoverLetterSystemFileName = coverLetterSystemFileName;
                header.CoverLetterFileName = Path.GetFileName(model.CoverLetter.FileName);
            }
        }

        [AllowAnonymous]
        public ActionResult Search(SearchViewModel model)
        {
            ViewBag.EmployerTypes = db.EmployerTypes.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            ViewBag.EmploymentTypes = db.EmploymentTypes.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            return View(model);
        }
    }
}
