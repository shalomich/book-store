using BookStore.Application.DbQueryConfigs.Specifications;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Domain.Entities.Books;
using BookStore.Domain.Entities.Products;
using BookStore.Persistance;
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
    public record GetByRandomAuthorQuery(DbFormEntityQueryBuilder<Book> Builder) : ISelectionQuery;
    internal class GetByRandomAuthorHandler : IRequestHandler<GetByRandomAuthorQuery, IEnumerable<Book>>
    {
        private ApplicationContext Context { get; }

        public GetByRandomAuthorHandler(ApplicationContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Book>> Handle(GetByRandomAuthorQuery request, CancellationToken cancellationToken)
        {
            var selectedAuthorOrder = Context.Set<AuthorSelectionOrder>()
                .OrderBy(order => order.Number)
                .LastOrDefault();

            if (selectedAuthorOrder == null)
                return Enumerable.Empty<Book>(); 

            return await request.Builder
                .AddSpecification(new ByAuthorIdSpecification(selectedAuthorOrder.AuthorId))
                .Build()
                .ToListAsync();
        }
    }
}
