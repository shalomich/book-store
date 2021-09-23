using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.ViewModels.Account
{
    public record AuthorizedData(string Token, string Role);
}
