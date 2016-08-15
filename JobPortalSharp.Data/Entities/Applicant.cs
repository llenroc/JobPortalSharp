using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalSharp.Data
{
    public enum Sexes
    {
        Male = 1,
        Female = 2
    }

    public class Applicant : Entity
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public Sexes Sex { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public float ExpectedSalary { get; set; }
    }
}
