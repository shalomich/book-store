using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Dto
{
    public record UserDto
    {
        public string UserName { init; get; }
        public string Email { init; get; }
        public string PhoneNumber { init; get; }
    }
}
