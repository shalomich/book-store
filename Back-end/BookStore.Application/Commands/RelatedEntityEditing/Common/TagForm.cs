using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Commands.RelatedEntityEditing.Common;
public record TagForm : RelatedEntityForm
{
    [Required]
    public int? TagGroupId { get; init; }
}

