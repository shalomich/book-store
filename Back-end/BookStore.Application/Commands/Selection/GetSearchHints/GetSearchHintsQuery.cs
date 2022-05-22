using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Domain.Entities.Books;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QueryWorker.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Selection.GetSearchHints
{
    public record GetSearchHintsQuery(SearchArgs SearchArgs, PaggingArgs PagingArgs) : IRequest<SearchHintsDto>;

    public class GetSearchHintsQueryHandler : IRequestHandler<GetSearchHintsQuery, SearchHintsDto>
    {
        private DbFormEntityQueryBuilder<Book> BookQueryBuilder { get; }
        private DbFormEntityQueryBuilder<Author> AuthorQueryBuilder { get; }
        private DbFormEntityQueryBuilder<Publisher> PublisherQueryBuilder { get; }

        public GetSearchHintsQueryHandler(DbFormEntityQueryBuilder<Book> bookQueryBuilder,
            DbFormEntityQueryBuilder<Author> authorQueryBuilder,
            DbFormEntityQueryBuilder<Publisher> publisherQueryBuilder)
        {
            BookQueryBuilder = bookQueryBuilder;
            AuthorQueryBuilder = authorQueryBuilder;
            PublisherQueryBuilder = publisherQueryBuilder;
        }

        public async Task<SearchHintsDto> Handle(GetSearchHintsQuery request, CancellationToken cancellationToken)
        {
            var (searchArgs, pagingArgs) = request;

            var searchBookHints = await BookQueryBuilder.AddSearch(searchArgs)
                .AddPagging(pagingArgs)
                .Build()
                .Select(book => book.Name)
                .ToArrayAsync();

            var searchAuthorHints = await AuthorQueryBuilder.AddSearch(searchArgs)
                .AddPagging(pagingArgs)
                .Build()
                .Select(author => author.Name)
                .ToArrayAsync();

            var searchPublisherHints = await PublisherQueryBuilder.AddSearch(searchArgs)
                .AddPagging(pagingArgs)
                .Build()
                .Select(publisher => publisher.Name)
                .ToArrayAsync();

            return new SearchHintsDto()
            {
                Books = searchBookHints,
                Authors = searchAuthorHints,
                Publishers = searchPublisherHints
            };
        }
    }
}
