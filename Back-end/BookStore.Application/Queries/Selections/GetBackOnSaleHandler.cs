using BookStore.Application.DbQueryConfigs.Specifications;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Domain.Entities.Books;
using BookStore.Domain.Entities.Products;
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
    public record GetBackOnSaleQuery(DbFormEntityQueryBuilder<Book> Builder) : ISelectionQuery;
    internal class GetBackOnSaleHandler : IRequestHandler<GetBackOnSaleQuery, IEnumerable<Book>>
    {
        public async Task<IEnumerable<Book>> Handle(GetBackOnSaleQuery request, CancellationToken cancellationToken)
        {
            return await request.Builder
                .AddSpecification(new BackOnSaleSpecification<Book>())
                .Build()
                .OrderBy(book => EF.Functions.DateDiffDay(book.ProductCloseout.ReplenishmentDate, DateTime.Now))
                .ToListAsync();
        }
    }
}
