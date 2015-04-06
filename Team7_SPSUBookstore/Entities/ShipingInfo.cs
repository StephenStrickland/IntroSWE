using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    class ShipingInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddr { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ToString()
        {
            string address =
@"{0}
{1}, {2} {3}";
            return String.Format(address, StreetAddr, City, State, ZipCode);
        }
    }
}
