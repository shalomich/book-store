using App.Areas.Dashboard.ViewModels.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Dashboard.ViewModels
{
    public record FormEntityIdentitiesByQuery
    {
        public FormEntityIdentity[] FormEntities { init; get; }
        public string[] QueryErrors { init; get; }
    }
}
