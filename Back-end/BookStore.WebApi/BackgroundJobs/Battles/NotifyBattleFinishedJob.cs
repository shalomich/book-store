using BookStore.Application.Notifications.BattleFinished;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.WebApi.BackgroundJobs.Battles;

internal class NotifyBattleFinishedJob
{
    private IMediator Mediator { get; }

    public NotifyBattleFinishedJob(IMediator mediator)
    {
        Mediator = mediator;
    }

    public async Task NotifyBattleFinished(int battleId, CancellationToken cancellationToken)
    {
        await Mediator.Publish(new BattleFinishedNotification(battleId), cancellationToken);
    }
}

