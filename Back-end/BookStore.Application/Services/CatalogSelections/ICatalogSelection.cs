using BookStore.Domain.Entities.Books;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services.CatalogSelections
{
    public interface ICatalogSelection
    {
        IQueryable<Book> Select(DbSet<Book> bookSet);
    }
}
