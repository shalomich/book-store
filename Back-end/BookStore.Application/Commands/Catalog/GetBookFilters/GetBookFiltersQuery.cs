using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Application.Commands.BookEditing.Common;
using BookStore.Application.Commands.Selection.Common;
using BookStore.Application.Dto;
using BookStore.Domain.Entities.Books;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QueryWorker;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Catalog.GetBookFilters;

public record GetBookFiltersQuery() : IRequest<BookFiltersDto>;
internal class GetBookFiltersQueryHandler : IRequestHandler<GetBookFiltersQuery, BookFiltersDto>
{
    private ApplicationContext Context { get; }
    private IMapper Mapper { get; }

    public GetBookFiltersQueryHandler(
        ApplicationContext context,
        IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }

    public async Task<BookFiltersDto> Handle(GetBookFiltersQuery request, CancellationToken cancellationToken)
    {
        var bookTypes = await Context.BookTypes
            .ProjectTo<RelatedEntityDto>(Mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var ageLimits = await Context.AgeLimits
            .ProjectTo<RelatedEntityDto>(Mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var coverArts = await Context.CoverArts
            .ProjectTo<RelatedEntityDto>(Mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var genres = await Context.Genres
            .ProjectTo<RelatedEntityDto>(Mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new BookFiltersDto
        {
            BookTypes = bookTypes,
            AgeLimits = ageLimits,
            CoverArts = coverArts,
            Genres = genres
        };
    }
}

