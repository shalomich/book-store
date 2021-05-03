using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Exceptions
{
    public class RestError
    {
        public string Reason { set; get; }
        public string Message { set; get; }
    }
}
