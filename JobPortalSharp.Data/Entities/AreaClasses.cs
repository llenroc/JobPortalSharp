using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalSharp.Data.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double? Lattitude { get; set; }
        public double? Longitude { get; set; }
    }

    public class State : Location
    {
        public string ShortName { get; set; }
    }

    public class Suburb : Location
    {
        public int? StateId { get; set; }
        public State State { get; set; }
    }
}
