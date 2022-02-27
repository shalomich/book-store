using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Application.Dto;
using BookStore.Application.Services;
using BookStore.Application.Services.CatalogSelections;
using BookStore.Domain.Entities.Books;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QueryWorker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Queries
{
    public record GetCatalogSelectionQuery(ICatalogSelection CatalogSelection, OptionParameters OptionParameters) : IRequest<PreviewSetDto>;
    internal class GetCatalogSelectionHandler : IRequestHandler<GetCatalogSelectionQuery, PreviewSetDto>
    {
        private LoggedUserAccessor LoggedUserAccessor { get; }
        private ApplicationContext Context { get; }
        private SelectionConfigurator<Book> SelectionConfigurator { get; }
        private IMapper Mapper { get; }

        public GetCatalogSelectionHandler(LoggedUserAccessor loggedUserAccessor, ApplicationContext context,
            SelectionConfigurator<Book> selectionConfigurator, IMapper mapper)
        {
            LoggedUserAccessor = loggedUserAccessor;
            Context = context;
            SelectionConfigurator = selectionConfigurator;
            Mapper = mapper;
        }
        public async Task<PreviewSetDto> Handle(GetCatalogSelectionQuery request, CancellationToken cancellationToken)
        {
            var (catalogSelection, optionParameters) = request;

            var (pagging, filters, sortings) = optionParameters;

            IQueryable<Book> catalogBooks = catalogSelection.Select(Context.Books);

            catalogBooks = SelectionConfigurator.AddFilters(catalogBooks, filters);

            int totalCount = await catalogBooks.CountAsync();

            catalogBooks = SelectionConfigurator.AddPagging(catalogBooks, pagging);
            catalogBooks = SelectionConfigurator.AddSorting(catalogBooks, sortings);

            var previews = await catalogBooks
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
    }
}
