
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
using BookStore.Application.DbQueryConfigs.SelectionFactories;
using BookStore.Domain.Enums;

namespace BookStore.Application.Queries
{
    public record ChooseSelectionQuery(Selection Selection, DbEntityQueryBuilder<Book> Builder) : IRequest<Unit>;
    internal class ChooseSelectionHandler : IRequestHandler<ChooseSelectionQuery, Unit>
    {
        public Task<Unit> Handle(ChooseSelectionQuery request, CancellationToken cancellationToken)
        {
            var (selection, builder) = request;

            ISelectionFactory selectionFactory = ChooseFactory(selection);

            builder
                .AddSpecification(selectionFactory.CreateSpecification())
                .AddOrder(selectionFactory.CreateOrder());

            return Unit.Task;
        }

        private ISelectionFactory ChooseFactory(Selection selection) => selection switch
        {
            Selection.Novelty => new NoveltyFactory(),
            Selection.GoneOnSale => new GoneOnSaleFactory(),
            Selection.BackOnSale => new BackOnSaleFactory(),
            Selection.ForChildren => new ForChildrenFactory(),
            Selection.CurrentDayAuthor => new CurrentDayAuthorFactory(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
