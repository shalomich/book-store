using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Commands.Tags.Common;
public record UserTagDto
{
    [Required]
    public int? TagId { get; init; }
}

