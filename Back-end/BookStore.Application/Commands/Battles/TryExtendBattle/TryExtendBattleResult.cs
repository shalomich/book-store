using System;

namespace BookStore.Application.Commands.Battles.TryExtendBattle;
public record TryExtendBattleResult()
{
    public bool NeedToExtend { get; init; }
    public DateTimeOffset? NewEndDate { get; init; }
}
