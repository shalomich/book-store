using App.Entities;
using QueryWorker.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Storage.ViewModels
{
    public abstract class EntitySortingConfig<T> : QueryConfiguration<T> where T : Entity
    {
        public EntitySortingConfig()
        {
            CreateSorting(entity => entity.Name);
        }
    }
}
