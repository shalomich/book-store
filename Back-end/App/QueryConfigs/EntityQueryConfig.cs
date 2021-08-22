using App.Entities;
using QueryWorker.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.QueryConfigs
{
    public abstract class EntityQueryConfig<T> : QueryConfiguration<T> where T : Entity
    {
        public EntityQueryConfig()
        {
            CreateSorting("name", entity => entity.Name);
            
            CreateFilter("name",entity => entity.Name);

            CreateSearch("name", entity => entity.Name);
        }
    }
}
