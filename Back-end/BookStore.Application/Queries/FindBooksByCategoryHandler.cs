
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
using BookStore.Application.DbQueryConfigs.CategoryFactories;
using BookStore.Domain.Enums;
using BookStore.Persistance;
using BookStore.Application.Extensions;
using BookStore.Application.Dto;
using QueryWorker;
using BookStore.Application.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace BookStore.Application.Queries
{
    public record FindBooksByCategoryQuery(Category Category, OptionParameters OptionParameters) : IRequest<PreviewSetDto>;
    internal class FindBooksByCategoryHandler : IRequestHandler<FindBooksByCategoryQuery, PreviewSetDto>
    {
        private LoggedUserAccessor LoggedUserAccessor { get; }
        private ApplicationContext Context { get; }
        private SelectionConfigurator<Book> SelectionConfigurator { get; }
        private IMapper Mapper { get; }

        public FindBooksByCategoryHandler(LoggedUserAccessor loggedUserAccessor, ApplicationContext context,
           SelectionConfigurator<Book> selectionConfigurator, IMapper mapper)
        {
            LoggedUserAccessor = loggedUserAccessor;
            Context = context;
            SelectionConfigurator = selectionConfigurator;
            Mapper = mapper;
        }

        public async Task<PreviewSetDto> Handle(FindBooksByCategoryQuery request, CancellationToken cancellationToken)
        {
            ICategoryFactory selectionFactory = ChooseFactory(request.Category);

            var categoryBooks = Context.Books
              .Where(selectionFactory.CreateSpecification()
                .ToExpression())
              .OrderBy(selectionFactory.CreateOrder());

            var (pagging, filters, sortings) = request.OptionParameters;

            categoryBooks = SelectionConfigurator.AddFilters(categoryBooks, filters);

            int totalCount = await categoryBooks.CountAsync();

            categoryBooks = SelectionConfigurator.AddPagging(categoryBooks, pagging);
            categoryBooks = SelectionConfigurator.AddSorting(categoryBooks, sortings);

            var previews = await categoryBooks
                .ProjectTo<PreviewDto>(Mapper.ConfigurationProvider)
                .ToArrayAsync();

            if (LoggedUserAccessor.IsAuthenticated())
            {
                int currentUserId = LoggedUserAccessor.GetCurrentUserId();

                var basketProductIds = await Context.BasketProducts
                    .Where(basketProduct => basketProduct.UserId == currentUserId)
                    .Select(basketProduct => basketProduct.ProductId)
                    .ToArrayAsync();

                foreach (var preview in previews)
                    preview.IsInBasket = basketProductIds.Contains(preview.Id);

            }

            return new PreviewSetDto(previews, totalCount);
        }

        private ICategoryFactory ChooseFactory(Category category) => category switch
        {
            Category.Novelty => new NoveltyFactory(),
            Category.GoneOnSale => new GoneOnSaleFactory(),
            Category.BackOnSale => new BackOnSaleFactory(),
            Category.ForChildren => new ForChildrenFactory(),
            Category.CurrentDayAuthor => new CurrentDayAuthorFactory(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
