using BookStore.Application.DbQueryConfigs.SelectionConfigs;
using BookStore.Application.DbQueryConfigs.Specifications;
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

namespace BookStore.Application.Queries
{
    public record GetSelectionQuery(ISelectionConfig Config, PaggingArgs Pagging, 
        IEnumerable<FilterArgs> Filters, IEnumerable<SortingArgs> Sortings) : IRequest<IEnumerable<Book>>;
    internal class GetSelectionHandler : IRequestHandler<GetSelectionQuery, IEnumerable<Book>>
    { 
        private DbFormEntityQueryBuilder<Book> Builder { get; }

        public GetSelectionHandler(DbFormEntityQueryBuilder<Book> builder)
        {
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        public async Task<IEnumerable<Book>> Handle(GetSelectionQuery request, CancellationToken cancellationToken)
        {
            var ( config, pagging, filters, sortings) = request;

            var query = Builder
                .AddSpecification(config.CreateSpecification())
                .AddFilters(filters)
                .AddSortings(sortings)
                .AddPagging(pagging)
                .Build();

            if (sortings == null || sortings.Any() == false)
            {
                var sorting = config.CreateSorting();
                query = sorting.Transform(query);
            }

            return await query.ToListAsync();
        }
    }
}
