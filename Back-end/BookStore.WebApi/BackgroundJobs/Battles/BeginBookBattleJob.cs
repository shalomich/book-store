using BookStore.Application.Commands.Battles.BeginBookBattle;
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
        await Mediator.Send(new BeginBookBattleCommand(), cancellationToken);
    } 
}

