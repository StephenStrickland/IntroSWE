using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ShippingInfo
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Street Address is required")]
        public string StreetAddr { get; set; }

        [Required(ErrorMessage = "Zip Code is required")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "A Zip Code is 5 digits long")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "City  is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }
    }
}
