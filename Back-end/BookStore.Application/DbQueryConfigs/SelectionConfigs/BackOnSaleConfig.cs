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

namespace BookStore.Application.DbQueryConfigs.SelectionConfigs
{
    public class BackOnSaleConfig : ISelectionConfig
    {
        public Sorting<Book> CreateSorting()
        {
            return new Sorting<Book>(book =>
                EF.Functions.DateDiffDay(book.ProductCloseout.ReplenishmentDate, DateTime.Now))
            {
                IsAscending = false
            };
        }

        public Specification<Book> CreateSpecification()
        {
            return new BackOnSaleSpecification<Book>();
        }
    }
}
