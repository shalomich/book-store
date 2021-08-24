using App.Entities;
using QueryWorker.DataTransformers.Paggings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Common.ViewModels
{
    public record FormEntitiesByQuery
    {
        public IQueryable<FormEntity> FormEntities { init; get; }
        public string[] QueryErrors { init; get; }
        public PaggingInfo PaggingInfo { init; get; }
    }
}
