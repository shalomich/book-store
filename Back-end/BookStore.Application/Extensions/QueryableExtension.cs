using Abp.Specifications;
using BookStore.Application.DbQueryConfigs.Orders;
using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Extensions
{
    public static class QueryableExtension
    {
        public static IQueryable<T> Shuffle<T>(this IQueryable<T> query)
        {
            return query.OrderBy(item => Guid.NewGuid());
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, IOrder<T> order) where T : IEntity
        {
            return order.Order(query);
        }
    }
}
