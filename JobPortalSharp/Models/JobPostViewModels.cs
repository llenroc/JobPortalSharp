using JobPortalSharp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobPortalSharp.Models
{
    public class JobPostViewModel
    {
        [Display(Name = "Job Title")]
        public string Name { get; set; }
        public string Details { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Salary { get; set; }

        [Display(Name = "From")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal SalaryRangeFrom { get; set; }

        [Display(Name = "To")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal SalaryRangeTo { get; set; }

        public DateTime? ExpirationDate { get; set; }

        [Display(Name = "Employment Type")]
        public int EmploymentTypeId { get; set; }
        public ICollection<EmploymentType> EmploymentTypes { get; set; }

        [Display(Name = "Industry")]
        public int IndustryId { get; set; }
        public ICollection<Industry> Industries { get; set; }
    }
}