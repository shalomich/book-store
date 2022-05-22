using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Application.Commands.BookEditing.Common;
using BookStore.Application.Dto;
using BookStore.Domain.Entities.Books;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QueryWorker;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Selection.Common;
public abstract record GetSelectionQuery(OptionParameters OptionParameters) : IRequest<PreviewSetDto>;
internal abstract class GetSelectionQueryHandler<T> : IRequestHandler<T, PreviewSetDto> where T : GetSelectionQuery
{
    public SelectionConfigurator<Book> SelectionConfigurator { get; }
    public IMapper Mapper { get; }
    public ImageFileRepository ImageFileRepository { get; }

    protected GetSelectionQueryHandler(
        SelectionConfigurator<Book> selectionConfigurator,
        IMapper mapper,
        ImageFileRepository imageFileRepository)
    {
        SelectionConfigurator = selectionConfigurator;
        Mapper = mapper;
        ImageFileRepository = imageFileRepository;
    }

    
    public async Task<PreviewSetDto> Handle(T request, CancellationToken cancellationToken)
    {
        var (pagging, filters, sortings) = request.OptionParameters;

        var selectionQuery = GetSelectionQuery(request);

        selectionQuery = SelectionConfigurator.AddFilters(selectionQuery, filters);

        int totalCount = await selectionQuery.CountAsync(cancellationToken);

        selectionQuery = SelectionConfigurator.AddSorting(selectionQuery, sortings);
        selectionQuery = SelectionConfigurator.AddPagging(selectionQuery, pagging);
        
        IEnumerable<PreviewDto> previews = await selectionQuery
            .ProjectTo<PreviewDto>(Mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        previews = await SetFileUrls(previews, cancellationToken);

        return new PreviewSetDto(previews, totalCount);
    }

    protected abstract IQueryable<Book> GetSelectionQuery(T request);

    private async Task<IEnumerable<PreviewDto>> SetFileUrls(IEnumerable<PreviewDto> previews, CancellationToken cancellationToken)
    {
        var previewsWithUrl = new List<PreviewDto>();

        foreach (var preview in previews)
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

