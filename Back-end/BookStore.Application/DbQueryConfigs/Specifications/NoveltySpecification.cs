using Abp.Specifications;
using BookStore.Domain.Entities.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.DbQueryConfigs.Specifications
{
    internal class NoveltySpecification : Specification<Book>
    {
        public override Expression<Func<Book, bool>> ToExpression()
        {
            return book => book.ReleaseYear == DateTime.Now.Year;
        }
    }
}
