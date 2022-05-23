using BookStore.Domain.Entities;
using System;
using System.Linq;

namespace BookStore.Application.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Shuffle<T>(this IQueryable<T> query)
        {
            return query.OrderBy(item => Guid.NewGuid());
        }

        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageSize, int pageNumber) where T : IEntity
        {
            return query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
        }
    }
}
