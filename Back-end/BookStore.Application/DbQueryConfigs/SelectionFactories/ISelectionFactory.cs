using Abp.Specifications;
using BookStore.Domain.Entities.Books;
using QueryWorker.DataTransformers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Application.DbQueryConfigs.Orders;

namespace BookStore.Application.DbQueryConfigs.SelectionFactories
{
    internal interface ISelectionFactory
    {
        ISpecification<Book> CreateSpecification();
        IOrder<Book> CreateOrder();
    }
}
