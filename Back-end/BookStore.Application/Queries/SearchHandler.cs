using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Application.Dto;
using BookStore.Application.Extensions;
using BookStore.Application.Services;
using BookStore.Domain.Entities.Books;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QueryWorker;
using QueryWorker.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Queries
{
    public record SearchQuery(SearchParameters SearchParameters) : IRequest<PreviewSetDto>;
    internal class SearchHandler : IRequestHandler<SearchQuery, PreviewSetDto>
    {
        private LoggedUserAccessor LoggedUserAccessor { get;}
        private ApplicationContext Context { get; }
        private SelectionConfigurator<Book> SelectionConfigurator { get; }
        private IMapper Mapper { get; }

        public SearchHandler(LoggedUserAccessor loggedUserAccessor, ApplicationContext context,
            SelectionConfigurator<Book> selectionConfigurator, IMapper mapper)
        {
            LoggedUserAccessor = loggedUserAccessor;
            Context = context;
            SelectionConfigurator = selectionConfigurator;
            Mapper = mapper;
        }

        public async Task<PreviewSetDto> Handle(SearchQuery request, CancellationToken cancellationToken)
        {
            var (search, pagging, filters, sortings) = request.SearchParameters;

            IQueryable<Book> searchBooks = Context.Books;

            if (search != null)
                searchBooks = SelectionConfigurator.AddSearch(searchBooks, search);

            searchBooks = SelectionConfigurator.AddFilters(searchBooks, filters);

            int totalCount = await searchBooks.CountAsync();

            searchBooks = SelectionConfigurator.AddPagging(searchBooks, pagging);
            searchBooks = SelectionConfigurator.AddSorting(searchBooks, sortings);

            searchBooks
              .Include(book => book.Author)
              .Include(book => book.Publisher)
              .Include(book => book.Album)
                .ThenInclude(album => album.Images);

            var previews = await searchBooks
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
