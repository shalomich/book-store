using System;

namespace BookStore.Application.Queries.Battle.GetBattleInfo;
public record BattleInfoDto
{
    public int Id { get; init; }
    public BattleBookPreviewDto FirstBattleBook { get; init; }
    public BattleBookPreviewDto SecondBattleBook { get; init; }
    public bool IsActive { get; init; }

    public DateTimeOffset EndDate { get; init; }
    public int? VotedBattleBookId { get; init; }
    public int? SpentVotingPointCount { get; init; }
}

