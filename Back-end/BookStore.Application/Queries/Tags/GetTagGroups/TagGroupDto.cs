using System.Collections.Generic;
using System.Linq;

namespace BookStore.Application.Queries.Tags.GetTagGroups;
public record TagGroupDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public IEnumerable<TagDto> Tags { get; init; } = Enumerable.Empty<TagDto>();
}
