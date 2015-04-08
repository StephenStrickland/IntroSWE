using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PaymentInfo : ShipingInfo
    {

        //        - ccNumber: String - expDateMonth: int - expDateYear: int
        //- ccv: int
        //+ validateCCNumber(): bool + validateExpDate(): bool
        //+ validateCCV(): bool

        public string CcNumber { get; set; }
        public int ExpDateMonth { get; set; }
        public int ExpDateYear { get; set; }
        public int CCV { get; set; }

        public bool ValidateCcNUmber()
        {
            return true;
        }

        public bool ValidateExpDate()
        {
            return true;
        }

        public bool ValidateCCV()
        {
            return true;
        }

    }
}
