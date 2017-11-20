using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobPortalSharp.Models
{
    public class SettingsViewModel
    {
        [Display(Name = "Website Title")]
        public string WebsiteTitle { get; set; }
        [Display(Name = "Home Page Welcome Message")]
        public string HomePageWelcomeMessage { get; set; }
        [Display(Name = "Home Page Welcome Message Subtext")]
        public string HomePageWelcomeMessageSubtext { get; set; }
        [Display(Name = "Home Page Bottom Text")]
        public string HomePageBottomText { get; set; }
        [Display(Name = "About Text")]
        public string AboutText { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase HomePageImage { get; set; }
        [Display(Name = "Footer Text")]
        public string FooterText { get; set; }
    }
}