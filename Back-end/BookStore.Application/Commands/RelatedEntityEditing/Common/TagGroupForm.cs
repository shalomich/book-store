using BookStore.Domain.Entities.Products;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Commands.RelatedEntityEditing.Common;
public record TagGroupForm : RelatedEntityForm
{
    [Required]
    [RegularExpression(TagGroup.ColorHexMask)]
    public string ColorHex { get; init; }
}

