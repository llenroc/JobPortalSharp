using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalSharp.Data
{
    public class JobApplication
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public Applicant Applicant { get; set; }
        public int JobPostId { get; set; }
        public JobPost JobPost { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public bool Withdrawn { get; set; }
        public DateTime? WithdrawnDate { get; set; }
    }

    public class JobApplication2
    {
        public int Id { get; set; }
        public int JobPostId { get; set; }
        public JobPost JobPost { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public string EmailAddress { get; set; }
        public string CvFileName { get; set; }
        public string CvSystemFileName { get; set; }
        public string CoverLetterFileName { get; set; }
        public string CoverLetterSystemFileName { get; set; }
    }
}
