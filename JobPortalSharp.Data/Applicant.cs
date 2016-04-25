using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalSharp.Data
{
    public class Applicant : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PhoneNumber { get; set; }
    }
}
