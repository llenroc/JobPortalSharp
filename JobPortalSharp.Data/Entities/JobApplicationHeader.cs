using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalSharp.Data
{
    public class JobApplicationHeader
    {
        public int Id { get; set; }
        public JobPost JobPost { get; set; }

        [Display(Name = "Application Date")]
        public DateTime? ApplicationDate { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        public string CvFileName { get; set; }
        public string CvSystemFileName { get; set; }
        public string CoverLetterFileName { get; set; }
        public string CoverLetterSystemFileName { get; set; }

        public ICollection<JobApplicationDetail> Details { get; set; }
    }
}
