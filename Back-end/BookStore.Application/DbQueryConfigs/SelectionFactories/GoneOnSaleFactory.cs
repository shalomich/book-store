﻿using Abp.Specifications;
using BookStore.Application.DbQueryConfigs.Specifications;
using BookStore.Domain.Entities.Books;
using QueryWorker.DataTransformers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Application.DbQueryConfigs.Orders;

namespace BookStore.Application.DbQueryConfigs.CategoryFactories
{
    internal class GoneOnSaleFactory : ICategoryFactory
    {
        public IOrder<Book> CreateOrder()
        {
            return new GoneOnSaleOrder();
        }

        public ISpecification<Book> CreateSpecification()
        {
            return new GoneOnSaleSpecification();
        }
    }
}
