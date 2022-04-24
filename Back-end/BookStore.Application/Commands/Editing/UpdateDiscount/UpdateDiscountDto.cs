using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Commands.Editing.UpdateDiscount;
public record UpdateDiscountDto
{
    [Range(0, 100)]
    public int DiscountPercentage { init; get; }
}

