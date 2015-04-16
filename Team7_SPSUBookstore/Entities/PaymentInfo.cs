using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PaymentInfo : ShippingInfo
    {

        //        - ccNumber: String - expDateMonth: int - expDateYear: int
        //- ccv: int
        //+ validateCCNumber(): bool + validateExpDate(): bool
        //+ validateCCV(): bool

        [Required(ErrorMessage = "Credit Card Number is required")]
        [RegularExpression(@"\d{16}", ErrorMessage = "The Credit Card Number must be 16 digits long")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "The Credit Card Number must be 16 digits long")]
        public string CcNumber { get; set; }

        [Required(ErrorMessage = "Month expiration date is required")]
        [RegularExpression(@"^(0?[1-9]|1[012])$", ErrorMessage = "Month is in form MM")]
        public int ExpDateMonth { get; set; }

        [Required(ErrorMessage = "Year expiration date is required")]
        [RegularExpression(@"^([1-9]\d|\d{3,})$", ErrorMessage = "Year is in form YY")]
        public int ExpDateYear { get; set; }

        [Required(ErrorMessage = "Security Code is required")]
        [RegularExpression(@"777", ErrorMessage = "Invalid Security Code")]
        public int CCV { get; set; }

        public string CCnumberStub
        {
            get
            {
                return CcNumber.ToString().Substring(12);
            }
        }



    }
}
