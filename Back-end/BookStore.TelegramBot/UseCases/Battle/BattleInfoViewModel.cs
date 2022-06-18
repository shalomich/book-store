using BookStore.Domain.Enums;

namespace BookStore.TelegramBot.UseCases.Battle;
internal record BattleInfoViewModel
{
    public BattleBookViewModel FirstBattleBook { get; init; }
    public BattleBookViewModel SecondBattleBook { get; init; }
    public string LeaderBattleBookName { get; init; }
    public BattleState State { get; init; }
    public TimeSpan LeftTime { get; init; }
    public int CurrentDiscount { get; init; }
}

