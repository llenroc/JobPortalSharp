using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalSharp.Data.Dto
{
    public class JobPostDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmployerName { get; set; }
        public string Details { get; set; }
        public decimal Salary { get; set; }
        public decimal SalaryRangeFrom { get; set; }
        public decimal SalaryRangeTo { get; set; }
        public string IndustryName { get; set; }
        public string EmploymentTypeName { get; set; }
    }
}
