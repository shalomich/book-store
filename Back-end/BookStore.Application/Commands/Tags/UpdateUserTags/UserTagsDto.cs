using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BookStore.Application.Commands.Tags.UpdateUserTags;
public record UserTagsDto
{
    [Required]
    public IEnumerable<int> TagIds { get; init; } = Enumerable.Empty<int>();
}

