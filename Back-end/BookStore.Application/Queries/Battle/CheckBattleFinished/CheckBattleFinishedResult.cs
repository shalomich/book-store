namespace BookStore.Application.Queries.Battle.CheckBattleFinished;
public record CheckBattleFinishedResult(int CurrentBattleId, int PreviousBattleId, bool IsFinished);

