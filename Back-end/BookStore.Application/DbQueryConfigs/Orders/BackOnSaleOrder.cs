using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Entities.Books;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.DbQueryConfigs.Orders
{
    internal class BackOnSaleOrder : IOrder<Book>
    {
        public IQueryable<Book> Order(IQueryable<Book> books)
        {
            return books.OrderByDescending(book =>
                EF.Functions.DateDiffDay(book.ProductCloseout.ReplenishmentDate, DateTime.Now));
        }
    }
}
