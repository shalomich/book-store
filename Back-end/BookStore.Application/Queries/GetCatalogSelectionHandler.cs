using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Application.Commands.BookEditing.Common;
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
        private SelectionConfigurator<Book> SelectionConfigurator { get; }
        private IMapper Mapper { get; }
        private ImageFileRepository ImageFileRepository { get; }

        public GetCatalogSelectionHandler(
            SelectionConfigurator<Book> selectionConfigurator, 
            IMapper mapper, 
            ImageFileRepository imageFileRepository)
        {
            SelectionConfigurator = selectionConfigurator;
            Mapper = mapper;
            ImageFileRepository = imageFileRepository;
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

            previews = await SetFileUrls(previews, cancellationToken);

            return new PreviewSetDto(previews, totalCount);
        }

        private async Task<IEnumerable<PreviewDto>> SetFileUrls(IEnumerable<PreviewDto> previews, CancellationToken cancellationToken)
        {
            var previewsWithUrl = new List<PreviewDto>();   

            foreach(var preview in previews)
            {
                var titleImage = preview.TitleImage with
                {
                    FileUrl = await ImageFileRepository.GetPresignedUrlForViewing(preview.TitleImage.Id, cancellationToken)
                };

                previewsWithUrl.Add(preview with { TitleImage = titleImage });
            }

            return previewsWithUrl;
        }
    }
}
