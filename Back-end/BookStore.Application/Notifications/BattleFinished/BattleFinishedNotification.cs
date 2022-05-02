using MediatR;

namespace BookStore.Application.Notifications.BattleFinished;
public record BattleFinishedNotification(int CurrentBattleId, int PreviousBattleId) : INotification;