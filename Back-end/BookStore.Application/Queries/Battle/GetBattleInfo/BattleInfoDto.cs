using System;

namespace BookStore.Application.Queries.Battle.GetBattleInfo;
public record BattleInfoDto
{
    public BattleBookPreviewDto FirstBattleBook { get; init; }
    public BattleBookPreviewDto SecondBattleBook { get; init; }
    public DateTimeOffset EndDate { get; init; }
}

