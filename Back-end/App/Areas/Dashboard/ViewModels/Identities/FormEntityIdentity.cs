using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Dashboard.ViewModels.Identities
{
    public record FormEntityIdentity
    {
        public int Id { init; get; }
        public string Name { init; get; }
    }
}
