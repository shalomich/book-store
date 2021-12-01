using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Application.DbQueryConfigs.Orders;
using BookStore.Domain.Entities;

namespace BookStore.Application.Services.DbQueryBuilders.DbQueryBuildItems
{
    internal class OrderBuildItem<T> : IQueryBuildItem<T> where T : IEntity
    {
        private IOrder<T> Order { get; }

        public OrderBuildItem(IOrder<T> order)
        {
            Order = order;
        }

        public void AddQuery(ref IQueryable<T> entities)
        {
            entities = Order.Order(entities);
        }
    }
}
