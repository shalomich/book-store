
using QueryWorker.Extensions;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace QueryWorker.DataTransformers
{

    internal class Sorting<T> : IDataTransformer<T> where T : class
    {
        private readonly Expression<Func<T, object>> _propertySelector;
        public bool IsAscending { set; get; } = true;
        public Sorting(Expression<Func<T, object>> propertySelector)
        {
            _propertySelector = propertySelector ?? throw new ArgumentNullException(nameof(propertySelector));
        }

        public IQueryable<T> Transform(IQueryable<T> query)
        {
            query =  IsAscending == true ? query.AppendOrderBy(_propertySelector).AsQueryable() 
                : query.AppendOrderByDescending(_propertySelector);

            return query;
        }
    }
}
