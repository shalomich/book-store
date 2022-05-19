using BookStore.Domain.Enums;
using System;

namespace BookStore.Application.Queries.Battle.GetBattleInfo;
public record BattleInfoDto
{
    public int Id { get; init; }
    public BattleBookPreviewDto FirstBattleBook { get; init; }
    public BattleBookPreviewDto SecondBattleBook { get; init; }
    public string State { get; init; }
    public DateTimeOffset EndDate { get; init; }
    public int InitialDiscount { get; init; }
    public int FinalDiscount { get; init; }
    public int? DiscountPercentage { get; init; }
}

