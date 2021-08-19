using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace QueryWorker.Extensions
{
    internal static class QueryableExtensions
    {
        public static IOrderedQueryable<T> AppendOrderBy<T, TKey>(this IQueryable<T> query, Expression<Func<T, TKey>> keySelector)
            => query.Expression.Type == typeof(IOrderedQueryable<T>)
            ? ((IOrderedQueryable<T>)query).ThenBy(keySelector)
            : query.OrderBy(keySelector);

        public static IOrderedQueryable<T> AppendOrderByDescending<T, TKey>(this IQueryable<T> query, Expression<Func<T, TKey>> keySelector)
            => query.Expression.Type == typeof(IOrderedQueryable<T>)
                ? ((IOrderedQueryable<T>)query).ThenByDescending(keySelector)
                : query.OrderByDescending(keySelector);

        public static IQueryable<T> Page<T>(this IQueryable<T> data, int pageSize, int pageNumber)
        {
            if (pageSize <= 0)
                throw new ArgumentException();
            
            if (pageNumber <= 0)
                throw new ArgumentException();

            return data.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
        }
    }
}
