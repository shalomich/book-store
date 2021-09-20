using App.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Requirements
{
    public class BookGenresIncludeRequirement : IIncludeRequirement<Book>
    {
        public void Include(ref IQueryable<Book> entities)
        {
            entities = entities
                .Include(book => book.GenresBooks)
                .ThenInclude(genreBook => genreBook.Genre);
        }
    }
}
