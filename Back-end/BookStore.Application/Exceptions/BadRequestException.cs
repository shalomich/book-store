using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BookStore.Application.Exceptions
{
    public class BadRequestException : RestException
    {
        public BadRequestException(string message, Exception inner = null) : base(message, inner)
        {
        }

        public override HttpStatusCode Code => HttpStatusCode.BadRequest;
    }
}
