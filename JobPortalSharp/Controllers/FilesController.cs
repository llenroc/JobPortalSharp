using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobPortalSharp.Data;

namespace JobPortalSharp.Controllers
{
    public class FilesController : Controller
    {
        JobPortalSharpDbContext db = new JobPortalSharpDbContext();

        public FilePathResult GetFile(string file)
        {
            var path = Server.MapPath("~/App_Data") + "/" + file;
            if (System.IO.File.Exists(path))
            {
                var obj = db.JobApplications.Single(x => x.CvSystemFileName == file);
                return new FilePathResult(path, "application/octet-stream")
                   {
                       FileDownloadName = obj.CvFileName
                   };
            }
            else
            {
                throw new Exception("File not found.");
            }

        }
    }
}