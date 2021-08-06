using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Auth.Exceptions
{
    public class RestException : Exception
    {
        public HttpStatusCode Code { get; }
        
        public IEnumerable<RestError> Errors;

        public RestException(HttpStatusCode code, IEnumerable<RestError> errors)
        {
            Code = code;
            Errors = errors ?? throw new ArgumentNullException(nameof(errors));
        }
    }
}
