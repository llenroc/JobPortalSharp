using System;
using System.Collections.Generic;
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
        public string Details { get; set; }
        public decimal Salary { get; set; }
        public decimal SalaryRangeFrom { get; set; }
        public decimal SalaryRangeTo { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int EmployerId { get; set; }
        public Employer Employer { get; set; }
    }
}
