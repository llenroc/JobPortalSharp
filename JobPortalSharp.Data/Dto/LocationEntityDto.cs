using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalSharp.Data.Dto
{
    public class LocationEntityDto
    {
        public int Id { get; set; }
        public string AddressStreet { get; set; }
        public string AddressTown { get; set; }
        public string AddressState { get; set; }
        public string AddressCountry { get; set; }
        public int? CountryId { get; set; }
        public string AddressPostalCode { get; set; }
        public double? AddressLongitude { get; set; }
        public double? AddressLatitude { get; set; }
    }
}
