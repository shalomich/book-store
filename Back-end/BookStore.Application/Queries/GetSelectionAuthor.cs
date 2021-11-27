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
using BookStore.Application.Extensions;

namespace BookStore.Application.Queries
{
    public record GetSelectionAuthorQuery() : IRequest<Author>;
    internal class GetSelectionAuthorHandler : IRequestHandler<GetSelectionAuthorQuery, Author>
    {
        private ApplicationContext Context { get; }

        public GetSelectionAuthorHandler(ApplicationContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Author> Handle(GetSelectionAuthorQuery request, CancellationToken cancellationToken)
        {
            var selectedAuthorOrder = await Context.Set<AuthorSelectionOrder>()
                .Include(order => order.Author)
                .OrderBy(order => order.Number)
                .LastOrDefaultAsync();

            return selectedAuthorOrder.Author;
        }
    }
}
