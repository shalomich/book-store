using BookStore.Domain.Entities;
using BookStore.Domain.Entities.Books;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Requirements
{
    public class BookAuthorIncludeRequirement : IIncludeRequirement<Book>
    {
        public void Include(ref IQueryable<Book> entities)
        {
            entities = entities.Include(book => book.Author);
        }
    }
}
