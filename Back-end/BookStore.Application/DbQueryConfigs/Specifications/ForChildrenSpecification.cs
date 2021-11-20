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
    internal class ForChildrenSpecification : Specification<Book>
    {
        public override Expression<Func<Book, bool>> ToExpression()
        {
            return book => (book.AgeLimit.Name == "0+" || book.AgeLimit.Name == "6+")
                && book.Type.Name == "Художественная литература"
                && book.GenresBooks.Any(genreBook => genreBook.Genre.Name == "Сказка");
        }
    }
}
