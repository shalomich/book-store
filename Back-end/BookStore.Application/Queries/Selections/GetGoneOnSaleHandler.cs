using BookStore.Application.DbQueryConfigs.Specifications;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Domain.Entities.Books;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Queries.Selections
{
    public record GetGoneOnSaleQuery(DbFormEntityQueryBuilder<Book> Builder) : IRequest<IEnumerable<Book>>;
    internal class GetGoneOnSaleHandler : IRequestHandler<GetGoneOnSaleQuery, IEnumerable<Book>>
    {
        public async Task<IEnumerable<Book>> Handle(GetGoneOnSaleQuery request, CancellationToken cancellationToken)
        {
            return await request.Builder
                .AddSpecification(new GoneOnSaleSpecification())
                .Build()
                .OrderByDescending(book => book.AddingDate)
                .ToListAsync();
        }
    }
}
