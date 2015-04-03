using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User
    {
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
    }
}
