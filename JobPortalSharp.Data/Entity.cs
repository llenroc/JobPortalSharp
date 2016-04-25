using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalSharp.Data
{
    public class Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}
