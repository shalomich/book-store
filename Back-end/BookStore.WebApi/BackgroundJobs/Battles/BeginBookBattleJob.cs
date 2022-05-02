using BookStore.Application.Exceptions;
using BookStore.Application.Notifications.BattleFinished;
using BookStore.Application.Queries.Battle.CheckBattleFinished;
using MediatR;
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
        
        var checkBattleFinishedResult = await Mediator.Send(new CheckBattleFinishedQuery(), cancellationToken);

        if (!checkBattleFinishedResult.IsFinished)
        {
            throw new BadRequestException("Battle hadn't finished yet.");
        }
        
        await Mediator.Publish(new BattleFinishedNotification(checkBattleFinishedResult.CurrentBattleId,
            checkBattleFinishedResult.PreviousBattleId));
    } 
}

