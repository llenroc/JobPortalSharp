using JobPortalSharp.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortalSharp.Services
{
    public class IdentityService
    {
        public static void AddUserToRole(string userId, string role, JobPortalSharpDbContext context)
        {
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);

            manager.AddToRole(userId, role);
        }
    }
}