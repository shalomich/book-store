using QueryWorker.Configurations;
using QueryWorker.QueryNodeParams;
using QueryWorker.QueryNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace QueryWorker
{

    internal class Sorting : IQueryNode
    {
        private SortingArgs _args;

        public Sorting(SortingArgs args)
        {
            _args = args ?? throw new ArgumentNullException(nameof(args));
        }

        public IQueryable<T> Execute<T>(IQueryable<T> query, QueryConfiguration config) where T : class
        {
            var (propertyName, isAscending) = _args;

            var propertySelector = config.GetSorting(propertyName);

            query =  isAscending == true ? (IQueryable<T>)query.AppendOrderBy(propertySelector).AsQueryable() 
                : (IQueryable<T>) query.AppendOrderByDescending(propertySelector);

            return query;
        }
    }
}
