using App.Areas.Dashboard.ViewModels;
using App.Entities;
using App.Entities.Books;
using QueryWorker.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.QueryConfigs
{
    public class BookQueryConfig : ProductQueryConfig<Book>
    {
        public BookQueryConfig()
        {
            CreateSorting("releaseYear", book => book.ReleaseYear);

            CreateFilter("releaseYear", book => book.ReleaseYear);
            CreateFilter("authorId", book => book.AuthorId);
            CreateFilter("publisherId", book => book.PublisherId);
            CreateFilter("typeId", book => book.TypeId.Value);
            CreateFilter("ageLimitId", book => book.AgeLimitId.Value);
            CreateFilter("coverArtId", book => book.CoverArtId.Value);
            CreateFilter("genreIds", book => book.GenresBooks
                .Select(genre => genre.Genre.Id));

            CreateSearch("author", entity => entity.Author.Name);
            CreateSearch("publisher", entity => entity.Publisher.Name);
        }
    }
}
