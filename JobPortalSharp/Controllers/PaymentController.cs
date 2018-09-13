using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobPortalSharp.Data;

namespace JobPortalSharp.Controllers
{
    public class ChargeViewModel
    {
        public int JobPostId { get; set; }
        public string StripeToken { get; set; }
        public string StripeEmail { get; set; }
    }

    public class PaymentController : Controller
    {
        JobPortalSharpDbContext _context = new JobPortalSharpDbContext();

        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Charge(ChargeViewModel model)
        {
            var jobPost = _context.JobPosts.SingleOrDefault(x => x.Id == model.JobPostId);
            if (jobPost != null)
            {
                jobPost.Paid = true;
                jobPost.StripeToken = model.StripeToken;
                jobPost.StripeEmail = model.StripeEmail;
                _context.SaveChanges();
            }
            return RedirectToAction("Confirmation");
        }

        public ActionResult Confirmation()
        {
            return View();
        }
    }
}
