using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Auth.ViewModels
{
    public record AuthorizedData(string Token, string Role);
}
