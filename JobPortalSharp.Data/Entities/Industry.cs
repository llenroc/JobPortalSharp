using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalSharp.Data.Entities
{
    public class IndustryCategory : Entity
    {
        public ICollection<Industry> Industries { get; set; }
    }

    public class Industry : Entity
    {
        public int? CategoryId { get; set; }
        public IndustryCategory Category { get; set; }
    }
}
