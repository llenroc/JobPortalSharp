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
        
        public int? p1 { get; set; }
        public int? p2 { get; set; }
        public int? ps { get; set; }
        public decimal? sf { get; set; }
        public decimal? st { get; set; }
        public int? ind { get; set; }
        public DateTime? dt { get; set; }

        public string l1 { get; set; } //location
        public double? latL1 { get; set; } //latitude
        public double? lngL1 { get; set; } //longitude
        public bool nb1 { get; set; } //nearby
        public int rc { get; set; } //result count

        public string l2 { get; set; } //location
        public double? latL2 { get; set; } //latitude
        public double? lngL2 { get; set; } //longitude
        public bool nb2 { get; set; } //nearby
        public int rc2 { get; set; } //result count

        public IEnumerable<JobPostDto> Results1 { get; set; }
        public IEnumerable<JobPostDto> Results2 { get; set; }
        public IEnumerable<int> ers { get; set; }
        public IEnumerable<int> ets { get; set; }
        public IList<SelectListItem> EmployerTypes { get; set; }
        public IList<SelectListItem> EmploymentTypes { get; set; }
    }
}