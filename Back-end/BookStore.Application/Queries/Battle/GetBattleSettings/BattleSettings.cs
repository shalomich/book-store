namespace BookStore.Application.Queries.Battle.GetBattleSettings;

public record BattleSettings
{
    public int BattleDurationInDays { get; init; }
    public int VotingPointCountBattleBeginning { get; init; }
    public double VotingPointCountPerRuble { get; init; }
    public int InitialDiscount { get; init; }
    public int FinalDiscount { get; init; }
    public double DiscountPerVotingPoint { get; init; }
    public int LowerBoundBookCost { get; init; }
    public int MaxBookCostDifference { get; init; }
}

