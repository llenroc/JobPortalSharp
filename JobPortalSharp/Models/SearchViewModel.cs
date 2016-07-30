using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JobPortalSharp.Data;

namespace JobPortalSharp.Models
{
    public class SearchViewModel
    {
        public string Keywords { get; set; }
        public int ResultCount { get; set; }
        public IEnumerable<JobPost> Posts { get; set; }
    }
}