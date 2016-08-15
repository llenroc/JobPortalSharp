using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JobPortalSharp.Data;
using JobPortalSharp.Data.Dto;

namespace JobPortalSharp.Models
{
    public class SearchViewModel
    {
        public string q { get; set; }
        public string l1 { get; set; }
        public string l2 { get; set; }
        public int? page { get; set; }
        public int ResultCount { get; set; }
        public IEnumerable<JobPostDto> Posts { get; set; }
    }
}