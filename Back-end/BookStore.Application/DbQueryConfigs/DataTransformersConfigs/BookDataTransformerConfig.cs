
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

            CreateRangeFilter(nameof(Book.ReleaseYear), book => book.ReleaseYear);
            CreatePlentyFilter(nameof(Book.Type), book => book.TypeId.Value);
            CreatePlentyFilter(nameof(Book.AgeLimit), book => book.AgeLimitId.Value);
            CreatePlentyFilter(nameof(Book.CoverArt), book => book.CoverArtId.Value);
            CreatePlentyFilter("genre", book => book.GenresBooks
                .Select(genre => genre.GenreId));
            CreatePlentyFilter("tag", book => book.BookTags
                .Select(bookTag => bookTag.TagId));

            CreateSearch("authorName", entity => entity.Author.Name);
            CreateSearch("publisherName", entity => entity.Publisher.Name);
        }
    }
}
