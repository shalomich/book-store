using BookStore.Application.Commands.Battles.BeginBookBattle;
using BookStore.Application.Notifications.BattleBegun;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.WebApi.BackgroundJobs.Battles;

internal class BeginBookBattleJob
{
    private IMediator Mediator { get; }

    public BeginBookBattleJob(IMediator mediator)
    {
        Mediator = mediator;
    }

    public async Task BeginBookBattle(CancellationToken cancellationToken)
    {
        
        int? battleId = await Mediator.Send(new BeginBookBattleCommand(), cancellationToken);
        
        if (battleId.HasValue)
        {
            await Mediator.Publish(new BattleBegunNotification(battleId.Value));
        }
    } 
}

