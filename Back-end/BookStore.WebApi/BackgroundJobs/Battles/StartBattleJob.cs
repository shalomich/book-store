using BookStore.Application.Commands.Battles;
using BookStore.Application.Commands.Battles.StartBattle;
using BookStore.Application.Notifications.BattleStarted;
using Hangfire;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.WebApi.BackgroundJobs.Battles;

internal class StartBattleJob
{
    private IMediator Mediator { get; }

    public StartBattleJob(IMediator mediator)
    {
        Mediator = mediator;
    }

    public async Task StartBattle(CancellationToken cancellationToken)
    {
        var startBattleResult = await Mediator.Send(new StartBattleCommand(), cancellationToken);

        var (battleId, endDate) = startBattleResult;

        BackgroundJob.Schedule<FinishBattleJob>(job => job.FinishBattle(default), endDate);

        BackgroundJob.Enqueue<NotifyBattleStartedJob>(job => job.NotifyBattleStarted(battleId, default));
    }
}

