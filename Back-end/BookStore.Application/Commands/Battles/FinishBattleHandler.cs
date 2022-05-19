using BookStore.Application.Exceptions;
using BookStore.Domain.Entities.Battles;
using BookStore.Domain.Enums;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
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
        Battle currentBattle;

        try
        {
            currentBattle = await Context.Battles
                .Where(battle => battle.State != BattleState.Finished)
                .SingleOrDefaultAsync(cancellationToken);
        }
        catch(InvalidOperationException)
        {
            throw new InvalidOperationException("There are several not finished battles."); 
        }

        if (currentBattle == null)
        {
            throw new BadRequestException("There is no battle with not finished state.");
        }
        
        currentBattle.State = BattleState.Finished;

        await Context.SaveChangesAsync(cancellationToken);

        return currentBattle.Id;    
    }
}

