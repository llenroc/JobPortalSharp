using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalSharp.Data
{
    public class JobApplicationDetail
    {
        public int Id { get; set; }
        public int JobPostId { get; set; }
        public JobPost JobPost { get; set; }
        public int JobApplicationHeaderId { get; set; }
        public JobApplicationHeader JobApplicationHeader { get; set; }
    }
}
