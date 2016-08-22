using JobPortalSharp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalSharp.Data
{
    public enum SalaryModes
    {
        PerHour,
        Monthly,
        Annually
    }

    public class JobPost : Entity
    {
        [Display(Name = "Job Title")]
        public override string Name { get; set; }

        public string Details { get; set; }
        public decimal Salary { get; set; }

        [Display(Name = "From")]
        public decimal SalaryRangeFrom { get; set; }

        [Display(Name = "To")]
        public decimal SalaryRangeTo { get; set; }

        public DateTime? PostDate { get; set; }

        [Display(Name = "Expiration Date")]
        public DateTime? ExpirationDate { get; set; }
        public int EmployerId { get; set; }
        public Employer Employer { get; set; }

        [Display(Name = "Employment Type")]
        public int EmploymentTypeId { get; set; }
        public EmploymentType EmploymentType { get; set; }

        [Display(Name = "Industry")]
        public int IndustryId { get; set; }
        public Industry Industry { get; set; }

        public ICollection<JobApplication2> Applications { get; set; }
    }
}
