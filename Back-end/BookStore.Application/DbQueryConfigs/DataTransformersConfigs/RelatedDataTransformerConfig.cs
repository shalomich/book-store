using BookStore.Domain.Entities;
using QueryWorker.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.DbQueryConfigs.DataTransformersConfigs
{
    public class RelatedEntityQueryConfig<T> : QueryConfiguration<T> where T : RelatedEntity
    {
        public RelatedEntityQueryConfig()
        {
            CreateSorting(nameof(RelatedEntity.Name), entity => entity.Name);
            
            CreateSearch(nameof(RelatedEntity.Name), entity => entity.Name);
        }
    }
}
