﻿using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Commands.BookEditing.Common;
public record DiscountForm
{
    [Range(1, 100)]
    public int Percentage { init; get; }

    [Required]
    public DateTimeOffset? EndDate { init; get; }
}

