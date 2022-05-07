using MediatR;

namespace BookStore.Application.Notifications.BattleStarted;
public record BattleStartedNotification(int BattleId) : INotification;
