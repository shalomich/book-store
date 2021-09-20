
using QueryWorker.Extensions;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace QueryWorker.DataTransformers
{

    internal class Sorting<T> : DataTransformer<T> where T : class
    {
        private Expression<Func<T, object>> PropertySelector { init; get; }
        public bool IsAscending { set; get; } = true;

        public Sorting()
        {
        }

        public Sorting(Expression<Func<T, object>> propertySelector)
        {
            PropertySelector = propertySelector ?? throw new ArgumentNullException(nameof(propertySelector));
        }

        public override IQueryable<T> Transform(IQueryable<T> query)
        {
            query =  IsAscending == true ? query.AppendOrderBy(PropertySelector).AsQueryable() 
                : query.AppendOrderByDescending(PropertySelector);

            return query;
        }
    }
}
