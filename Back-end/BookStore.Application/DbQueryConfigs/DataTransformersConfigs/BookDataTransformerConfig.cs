
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

            CreateRangeFilter("releaseYear", book => book.ReleaseYear);
            CreatePlentyFilter("authorId", book => book.AuthorId);
            CreatePlentyFilter("publisherId", book => book.PublisherId);
            CreatePlentyFilter("typeId", book => book.TypeId.Value);
            CreatePlentyFilter("ageLimitId", book => book.AgeLimitId.Value);
            CreatePlentyFilter("coverArtId", book => book.CoverArtId.Value);
            CreatePlentyFilter("genreIds", book => book.GenresBooks
                .Select(genre => genre.Genre.Id));

            CreateSearch(nameof(Book.AuthorId), entity => entity.Author.Name);
            CreateSearch(nameof(Book.PublisherId), entity => entity.Publisher.Name);
        }
    }
}
