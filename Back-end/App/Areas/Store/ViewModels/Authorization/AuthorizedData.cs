using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Store.ViewModels.Authorization
{
    public record AuthorizedData(string Token, string Role);
}
