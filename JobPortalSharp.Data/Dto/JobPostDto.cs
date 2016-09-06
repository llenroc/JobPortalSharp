using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalSharp.Data.Dto
{
    public class JobPostDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int EmployerId { get; set; }
        [Display(Name="Employer Name")]
        public string EmployerName { get; set; }
        public string Details { get; set; }
        public decimal Salary { get; set; }

        [Display(Name = "From")]
        public decimal SalaryRangeFrom { get; set; }

        [Display(Name = "To")]
        public decimal SalaryRangeTo { get; set; }

        [Display(Name = "Industry")]
        public string IndustryName { get; set; }

        [Display(Name = "Employment Type")]
        public string EmploymentTypeName { get; set; }

        [Display(Name = "Expiration Date")]
        public DateTime? ExpirationDate { get; set; }

        [Display(Name = "Number of Applications")]
        public int NumOfApplications { get; set; }
    }
}
