using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.Extensions
{
    public static class EnumerableExtension
    {
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }

        public static IQueryable<T> Shuffle<T>(this IQueryable<T> source)
        {
            return source.OrderBy(item => Guid.NewGuid());
        } 
    }
}
