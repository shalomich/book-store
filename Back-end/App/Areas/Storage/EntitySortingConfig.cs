using App.Entities;
using QueryWorker.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Storage.ViewModels
{
    public abstract class EntitySortingConfig<T> : QueryConfiguration where T : Entity
    {
        public EntitySortingConfig()
        {
            CreateSorting<T,string>(entity => entity.Name);
        }
    }
}
