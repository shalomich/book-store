using BookStore.Application.Notifications.BattleStarted;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.WebApi.BackgroundJobs.Battles;

internal class NotifyBattleStartedJob
{
    private IMediator Mediator { get; }

    public NotifyBattleStartedJob(IMediator mediator)
    {
        Mediator = mediator;
    }

    public async Task NotifyBattleStarted(int battleId, CancellationToken cancellationToken)
    {
        await Mediator.Publish(new BattleStartedNotification(battleId), cancellationToken);
    }
}

