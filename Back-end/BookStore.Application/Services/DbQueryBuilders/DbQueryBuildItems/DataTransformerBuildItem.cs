using BookStore.Domain.Entities;
using QueryWorker.DataTransformers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.Services.DbQueryBuilders.DbQueryBuildItems
{
    internal class DataTransformerBuildItem<T> : IQueryBuildItem<T> where T : class, IFormEntity
    {
        private IDataTransformer<T> _dataTransformer;

        public DataTransformerBuildItem(IDataTransformer<T> dataTransformer)
        {
            _dataTransformer = dataTransformer ?? throw new ArgumentNullException(nameof(dataTransformer));
        }

        public void AddQuery(ref IQueryable<T> entities)
        {
            entities = _dataTransformer.Transform(entities);
        }
    }
}
