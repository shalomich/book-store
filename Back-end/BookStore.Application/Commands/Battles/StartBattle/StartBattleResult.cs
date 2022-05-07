using System;

namespace BookStore.Application.Commands.Battles.StartBattle;

public record StartBattleResult(int BattleId, DateTimeOffset EndDate);
