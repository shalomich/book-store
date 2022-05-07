using BookStore.Application.Commands.Battles;
using Hangfire;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.WebApi.BackgroundJobs.Battles;

internal class FinishBattleJob
{
    private IMediator Mediator { get; }

    public FinishBattleJob(IMediator mediator)
    {
        Mediator = mediator;
    }

    public async Task FinishBattle(CancellationToken cancellationToken)
    {
        var finishedBattleId = await Mediator.Send(new FinishBattleCommand(), cancellationToken);

        BackgroundJob.Enqueue<NotifyBattleFinishedJob>(job => job.NotifyBattleFinished(finishedBattleId, default));

        BackgroundJob.Enqueue<StartBattleJob>(job => job.StartBattle(default));
    }
}

