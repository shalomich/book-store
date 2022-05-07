using BookStore.Domain.Enums;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Battles;

public record FinishBattleCommand() : IRequest<int>;
internal class FinishBattleHandler : IRequestHandler<FinishBattleCommand, int>
{
    private ApplicationContext Context { get; }
 
    public FinishBattleHandler(ApplicationContext context)
    {
        Context = context;
    }

    public async Task<int> Handle(FinishBattleCommand request, CancellationToken cancellationToken)
    {
        var currentBattle = await Context.Battles
            .Where(battle => battle.State != BattleState.Finished)
            .SingleAsync(cancellationToken);

        currentBattle.State = BattleState.Finished;

        await Context.SaveChangesAsync(cancellationToken);

        return currentBattle.Id;    
    }
}

