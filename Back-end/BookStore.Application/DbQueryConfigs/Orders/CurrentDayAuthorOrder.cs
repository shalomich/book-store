using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Application.Extensions;
using BookStore.Domain.Entities.Books;

namespace BookStore.Application.DbQueryConfigs.Orders
{
    internal class CurrentDayAuthorOrder : IOrder<Book>
    {
        public IQueryable<Book> Order(IQueryable<Book> books)
        {
            return books.Shuffle();
        }
    }
}
