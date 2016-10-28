using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using JobPortalSharp.Data;

namespace JobPortalSharp.Controllers.api
{
    public class ApiControllerBase : ApiController
    {
        protected JobPortalSharpDbContext db = new JobPortalSharpDbContext();
    }
}