using BookStore.Domain.Entities;
using QueryWorker.DataTransformers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services.QueryBuilders
{
    public class DataTransformerBuildItem<T> : IQueryBuildItem<T> where T : class, IFormEntity
    {
        private DataTransformer<T> _dataTransformer;

        public DataTransformerBuildItem(DataTransformer<T> dataTransformer)
        {
            _dataTransformer = dataTransformer ?? throw new ArgumentNullException(nameof(dataTransformer));
        }

        public void AddQuery(ref IQueryable<T> entities)
        {
            entities = _dataTransformer.Transform(entities);
        }
    }
}
