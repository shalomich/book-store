using MediatR;

namespace BookStore.Application.Notifications.BattleBegun;
public record BattleBegunNotification(int BattleId) : INotification;