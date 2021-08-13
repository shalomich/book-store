using QueryWorker.QueryNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace QueryWorker
{
    internal class Sorting<T> : IQueryNode<T> where T : class
    {
        public Expression<Func<T, object>> _propertySelector;
        public bool _isAscending;

        public Sorting(Expression<Func<T, object>> propertySelector, bool isAscending = true)
        {
            _propertySelector = propertySelector ?? throw new ArgumentNullException(nameof(propertySelector));
            _isAscending = isAscending;
        }

        public IQueryable<T> Execute(IQueryable<T> query)
        {
            query = _isAscending == true ? query.AppendOrderBy(_propertySelector) 
                : query.AppendOrderByDescending(_propertySelector);

            return query;
        }
    }
}
