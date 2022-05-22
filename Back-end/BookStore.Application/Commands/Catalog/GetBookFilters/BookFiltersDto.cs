using BookStore.Application.Dto;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Application.Commands.Catalog.GetBookFilters;
public record BookFiltersDto
{
    public IEnumerable<RelatedEntityDto> BookTypes { get; init; } = Enumerable.Empty<RelatedEntityDto>();
    public IEnumerable<RelatedEntityDto> AgeLimits { get; init; } = Enumerable.Empty<RelatedEntityDto>();
    public IEnumerable<RelatedEntityDto> CoverArts { get; init; } = Enumerable.Empty<RelatedEntityDto>();
    public IEnumerable<RelatedEntityDto> Genres { get; init; } = Enumerable.Empty<RelatedEntityDto>();
}

