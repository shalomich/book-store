using Abp.Specifications;
using BookStore.Application.DbQueryConfigs.Specifications;
using BookStore.Domain.Entities.Books;
using Microsoft.EntityFrameworkCore;
using QueryWorker.DataTransformers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Application.DbQueryConfigs.Orders;

namespace BookStore.Application.DbQueryConfigs.SelectionFactories
{
    internal class BackOnSaleFactory : ISelectionFactory
    {
        public IOrder<Book> CreateOrder()
        {
            return new BackOnSaleOrder();
        }

        public ISpecification<Book> CreateSpecification()
        {
            return new BackOnSaleSpecification<Book>();
        }
    }
}
