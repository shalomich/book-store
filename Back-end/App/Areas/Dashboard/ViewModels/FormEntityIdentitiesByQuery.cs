using App.Areas.Dashboard.ViewModels.Identities;
using QueryWorker.DataTransformers.Paggings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Dashboard.ViewModels
{
    public record FormEntityIdentitiesByQuery
    {
        public FormEntityIdentity[] FormEntityIdentities { init; get; }
        public string[] QueryErrors { init; get; }
        public PaggingInfo PaggingInfo { init; get; }

    }
}
