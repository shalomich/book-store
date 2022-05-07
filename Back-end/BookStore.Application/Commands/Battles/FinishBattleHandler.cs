using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Battles;

public record FinishBattleCommand() : IRequest;
internal class FinishBattleHandler : AsyncRequestHandler<FinishBattleCommand>
{
    private ApplicationContext Context { get; }
 
    public FinishBattleHandler(ApplicationContext context)
    {
        Context = context;
    }

    protected override async Task Handle(FinishBattleCommand request, CancellationToken cancellationToken)
    {
        var currentBattle = await Context.Battles
            .Where(battle => battle.IsActive)
            .SingleAsync(cancellationToken);

        currentBattle.IsActive = false;

        await Context.SaveChangesAsync(cancellationToken);
    }
}

