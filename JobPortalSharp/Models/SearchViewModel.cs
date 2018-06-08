using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobPortalSharp.Data;
using JobPortalSharp.Data.Dto;

namespace JobPortalSharp.Models
{
    public class SearchViewModel
    {
        public string q { get; set; }

        public int draw { get; set; }
        public int length { get; set; }
        public int start { get; set; }

        public decimal? sf { get; set; } //salary from
        public decimal? st { get; set; } //salary to
        public int? ind { get; set; } //industry
        public DateTime? dt { get; set; } //expiry filter
        public int rc { get; set; } //result count
        public int sort { get; set; }

        public string l { get; set; } //location
        public double? lat { get; set; } //latitude
        public double? lng { get; set; } //longitude
        public bool nb { get; set; } //nearby

        public IEnumerable<int> ers { get; set; }
        public IEnumerable<int> ets { get; set; }
    }
    public class HomeViewModel
    {
        public string WebsiteTitle { get; set; }
        public string HomePageWelcomeMessage { get; set; }
        public string HomePageWelcomeMessageSubtext { get; set; }
        public string HomePageBottomText { get; set; }
        public string AboutText { get; set; }
        public string FooterText { get; set; }

        public IEnumerable<SelectListItem> EmployerTypes { get; set; }
        public IEnumerable<SelectListItem> EmploymentTypes { get; set; }
    }

    public class FilterViewModel
    {
        public IEnumerable<SelectListItem> EmployerTypes { get; set; }
        public IEnumerable<SelectListItem> EmploymentTypes { get; set; }
    }
}