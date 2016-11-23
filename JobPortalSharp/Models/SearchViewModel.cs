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
        public string l1 { get; set; }
        public string l2 { get; set; }
        public int? p { get; set; }
        public int? ps { get; set; }
        public int ResultCount { get; set; }
        public decimal? sf { get; set; }
        public decimal? st { get; set; }
        public int? ind { get; set; }
        public DateTime? dt { get; set; }

        public IEnumerable<JobPostDto> Posts { get; set; }
        public IEnumerable<int> ers { get; set; }
        public IEnumerable<int> ets { get; set; }
        public IList<SelectListItem> EmployerTypes { get; set; }
        public IList<SelectListItem> EmploymentTypes { get; set; }
    }
}