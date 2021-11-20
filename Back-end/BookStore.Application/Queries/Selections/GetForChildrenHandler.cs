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
    public record GetForChildrenQuery(DbFormEntityQueryBuilder<Book> Builder) : IRequest<IEnumerable<Book>>;
    internal class GetForChildrenHandler : IRequestHandler<GetForChildrenQuery, IEnumerable<Book>>
    {
        public async Task<IEnumerable<Book>> Handle(GetForChildrenQuery request, CancellationToken cancellationToken)
        {
            return await request.Builder
                .AddSpecification(new ForChildrenSpecification())
                .Build()
                .OrderByDescending(book => book.AddingDate)
                .ToListAsync();
        }
    }
}
