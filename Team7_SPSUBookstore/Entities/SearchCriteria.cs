using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class SearchCriteria
    {
        public string BasicSearch { get; set; }
        public int CRN { get; set; }
        public int Section { get; set; }
        public string Semester { get; set; }
        public string Professor { get; set; }
        public string Course { get; set; }
        public bool isRequired { get; set; }
        public bool isAdvanced { get; set; }
        //public string Edition { get; set; }
        //public decimal Price { get; set; }
    }
}
