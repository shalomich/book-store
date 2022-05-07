using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Commands.Editing.UpdateDiscount;
public record DiscountForm
{
    [Range(0, 100)]
    public int Percentage { init; get; }

    public DateTimeOffset EndDate { init; get; }
}

