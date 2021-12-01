using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Entities;

namespace BookStore.Application.DbQueryConfigs.Orders
{
    public interface IOrder<T> where  T : IEntity
    {
        IQueryable<T> Order(IQueryable<T> entities);
    }
}
