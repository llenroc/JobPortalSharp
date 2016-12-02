using JobPortalSharp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalSharp.Data
{
    public enum NumberOfEmployees
    {
        [Display(Name="1-10")]
        N1_10,

        [Display(Name = "10-100")]
        N10_100,

        [Display(Name = "100-1000")]
        N100_1000,

        [Display(Name = "1000-10000")]
        N1000_10000,

        [Display(Name = "10000-100000")]
        N10000_100000,

        [Display(Name = "100000 and Above")]
        N100000_Above
    }

    public class Employer : Entity
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string CompanyDescription { get; set; } //information about the company
        
        public string AddressStreet { get; set; }
        public string AddressTown { get; set; }
        public string AddressState { get; set; }
        public string AddressCountry { get; set; }
        public double? AddressLongitude { get; set; }
        public double? AddressLatitude { get; set; }

        public NumberOfEmployees NumberOfEmployees { get; set; }
        public string CompanyLogoFileName { get; set; }
        public string CompanyLogoSystemFileName { get; set; }

        public ICollection<JobPost> JobPosts { get; set; }
    }
}
