using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Application.Dto;
using BookStore.Application.Services;
using BookStore.Application.Services.CatalogSelections;
using BookStore.Domain.Entities.Battles;
using BookStore.Domain.Entities.Books;
using BookStore.Domain.Enums;
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
    public record GetCatalogSelectionQuery(IBookSelection CatalogSelection, OptionParameters OptionParameters) : IRequest<PreviewSetDto>;
    internal class GetCatalogSelectionHandler : IRequestHandler<GetCatalogSelectionQuery, PreviewSetDto>
    {
        private LoggedUserAccessor LoggedUserAccessor { get; }
        private ApplicationContext Context { get; }
        private SelectionConfigurator<Book> SelectionConfigurator { get; }
        private IMapper Mapper { get; }
        private S3Storage S3Storage { get; }

        public GetCatalogSelectionHandler(LoggedUserAccessor loggedUserAccessor, ApplicationContext context,
            SelectionConfigurator<Book> selectionConfigurator, IMapper mapper, S3Storage s3Storage)
        {
            LoggedUserAccessor = loggedUserAccessor;
            Context = context;
            SelectionConfigurator = selectionConfigurator;
            Mapper = mapper;
            S3Storage = s3Storage;
        }

        public async Task<PreviewSetDto> Handle(GetCatalogSelectionQuery request, CancellationToken cancellationToken)
        {
            var (catalogSelection, optionParameters) = request;

            var (pagging, filters, sortings) = optionParameters;

            IQueryable<Book> catalogBooks = catalogSelection.Select();

            catalogBooks = SelectionConfigurator.AddFilters(catalogBooks, filters);

            int totalCount = await catalogBooks.CountAsync();

            catalogBooks = SelectionConfigurator.AddPagging(catalogBooks, pagging);
            catalogBooks = SelectionConfigurator.AddSorting(catalogBooks, sortings);

            IEnumerable<PreviewDto> previews = await catalogBooks
                .ProjectTo<PreviewDto>(Mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            previews = SetFileUrls(previews);

            previews = await SetBattleStatus(previews, cancellationToken);

            if (LoggedUserAccessor.IsAuthenticated())
            {
                int currentUserId = LoggedUserAccessor.GetCurrentUserId();

                previews = await SetBasketStatus(previews, currentUserId, cancellationToken);
            }

            return new PreviewSetDto(previews, totalCount);
        }

        private IEnumerable<PreviewDto> SetFileUrls(IEnumerable<PreviewDto> previews)
        {
            foreach(var preview in previews)
            {

                var titleImage = preview.TitleImage with
                {
                    FileUrl = S3Storage.GetPresignedUrlForViewing(preview.Id, preview.TitleImage.Id)
                };

                yield return preview with { TitleImage = titleImage };
            }
        }


        private async Task<IEnumerable<PreviewDto>> SetBasketStatus(IEnumerable<PreviewDto> previews, int currentUserId,
            CancellationToken cancellationToken)
        {
            var basketProductIds = await Context.BasketProducts
                .Where(basketProduct => basketProduct.UserId == currentUserId)
                .Select(basketProduct => basketProduct.ProductId)
                .ToArrayAsync(cancellationToken);

            var previewsWithBasketStatus = new List<PreviewDto>();

            foreach (var preview in previews)
            {
                bool isInBasket = basketProductIds.Contains(preview.Id);
                previewsWithBasketStatus.Add(preview with { IsInBasket = isInBasket });
            }
           
            return previewsWithBasketStatus;
        }

        private async Task<IEnumerable<PreviewDto>> SetBattleStatus(IEnumerable<PreviewDto> previews,
            CancellationToken cancellationToken)
        {
            var battleBookIds = await Context.Set<BattleBook>()
                .Where(battleBook => battleBook.Battle.State != BattleState.Finished)
                .Select(battleBook => battleBook.BookId)
                .ToListAsync(cancellationToken);

            var previewsWithBattleStatus = new List<PreviewDto>();

            foreach (var preview in previews)
            {
                bool isInBattle = battleBookIds.Contains(preview.Id);
                previewsWithBattleStatus.Add(preview with { IsInBattle = isInBattle });
            }

            return previewsWithBattleStatus;
        }


    }
}
