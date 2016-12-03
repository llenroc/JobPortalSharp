using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobPortalSharp.Data;
using System.IO;

namespace JobPortalSharp.Controllers
{
    public class FilesController : Controller
    {
        JobPortalSharpDbContext db = new JobPortalSharpDbContext();

        public FilePathResult GetCv(int id)
        {
            var jobApplication = db.JobApplicationHeaders.Single(x => x.Id == id);
            var path = Server.MapPath("~/App_Data/application_files") + "/" + jobApplication.CvSystemFileName;
            if (System.IO.File.Exists(path))
            {
                return new FilePathResult(path, "application/octet-stream")
                   {
                       FileDownloadName = jobApplication.CvFileName
                   };
            }
            else
            {
                throw new Exception("File not found.");
            }

        }

        public FilePathResult GetCl(int id)
        {
            var jobApplication = db.JobApplicationHeaders.Single(x => x.Id == id);
            var path = Server.MapPath("~/App_Data/application_files") + "/" + jobApplication.CoverLetterSystemFileName;
            if (System.IO.File.Exists(path))
            {
                return new FilePathResult(path, "application/octet-stream")
                   {
                       FileDownloadName = jobApplication.CoverLetterFileName
                   };
            }
            else
            {
                throw new Exception("File not found.");
            }

        }

        public ActionResult EmployerLogo(int id)
        {
            var emp = db.Employers.Single(e => e.Id == id);
            var path = Server.MapPath("~/App_Data/employer_logo") + "/" + emp.CompanyLogoSystemFileName;
            return File(path, "application/octet-stream", emp.CompanyLogoFileName);
        }
    }
}