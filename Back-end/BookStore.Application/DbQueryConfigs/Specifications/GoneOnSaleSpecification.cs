using Abp.Specifications;
using BookStore.Domain.Entities.Books;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.DbQueryConfigs.Specifications
{
    internal class GoneOnSaleSpecification : Specification<Book>
    {
        private const int GoneOnSaleDaysCount = 7;
        public override Expression<Func<Book, bool>> ToExpression()
        {
            return book => EF.Functions.DateDiffDay(book.AddingDate, DateTime.Now) < GoneOnSaleDaysCount; 
        }
    }
}
