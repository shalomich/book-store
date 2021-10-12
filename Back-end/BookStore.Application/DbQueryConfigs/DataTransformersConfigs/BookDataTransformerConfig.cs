
using BookStore.Domain.Entities;
using BookStore.Domain.Entities.Books;
using QueryWorker.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.DbQueryConfigs.DataTransformersConfigs
{
    public class BookDataTransformerConfig : ProductDataTransformerConfig<Book>
    {
        public BookDataTransformerConfig()
        {
            CreateSorting(nameof(Book.ReleaseYear), book => book.ReleaseYear);

            CreateFilter(nameof(Book.ReleaseYear), book => book.ReleaseYear);
            CreateFilter(nameof(Book.AuthorId), book => book.AuthorId);
            CreateFilter(nameof(Book.PublisherId), book => book.PublisherId);
            CreateFilter(nameof(Book.TypeId), book => book.TypeId.Value);
            CreateFilter(nameof(Book.AgeLimitId), book => book.AgeLimitId.Value);
            CreateFilter(nameof(Book.CoverArtId), book => book.CoverArtId.Value);
            CreateFilter("genreIds", book => book.GenresBooks
                .Select(genre => genre.Genre.Id));

            CreateSearch(nameof(Book.AuthorId), entity => entity.Author.Name);
            CreateSearch(nameof(Book.PublisherId), entity => entity.Publisher.Name);
        }
    }
}
