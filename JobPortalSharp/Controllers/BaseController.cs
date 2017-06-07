using JobPortalSharp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobPortalSharp.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            Database = new JobPortalSharpDbContext();
        }

        protected JobPortalSharpDbContext Database { get; set; }

        protected override void Dispose(bool disposing)
        {
            Database.Dispose();
            base.Dispose();
        }
    }
}