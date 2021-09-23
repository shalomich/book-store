using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BookStore.Application.Exceptions
{
    public abstract class RestException : Exception
    {
        public abstract HttpStatusCode Code { get; }
       
        public RestException(string message, Exception inner = null) : base(message, inner)
        {
        }
    }
}
