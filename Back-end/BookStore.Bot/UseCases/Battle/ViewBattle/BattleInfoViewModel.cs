using BookStore.Domain.Enums;

namespace BookStore.Bot.UseCases.Battle.ViewBattle;
internal record BattleInfoViewModel
{
    public BattleBookViewModel FirstBattleBook { get; init; }
    public BattleBookViewModel SecondBattleBook { get; init; }
    public string LeaderBattleBookName { get; init; }
    public BattleState State { get; init; }
    public TimeSpan LeftTime { get; init; }
    public int CurrentDiscount { get; init; }
}

