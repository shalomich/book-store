using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Dto
{
    public record OrderUserInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; init; }
        public string PhoneNumber { get; init; }
        public string Address { get; init; }
    }
}
