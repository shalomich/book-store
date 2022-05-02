using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Queries.Battle.CheckBattleFinished;

public record CheckBattleFinishedQuery() : IRequest<CheckBattleFinishedResult>;
internal class CheckBattleFinishedHandler : IRequestHandler<CheckBattleFinishedQuery, CheckBattleFinishedResult>
{
    private ApplicationContext Context { get; }

    public CheckBattleFinishedHandler(ApplicationContext context)
    {
        Context = context;
    }

    public async Task<CheckBattleFinishedResult> Handle(CheckBattleFinishedQuery request, CancellationToken cancellationToken)
    {
        var battles = await Context.Battles
            .OrderByDescending(battle => battle.EndDate)
            .Take(2)
            .ToListAsync(cancellationToken);

        var currentBattle = battles.First();
        var previousBattle = battles.Last();

        if (!currentBattle.IsActive)
        {
            throw new InvalidOperationException("Current battle is not active.");
        }

        bool battleFinished = currentBattle.EndDate < DateTimeOffset.Now;

        return new CheckBattleFinishedResult(currentBattle.Id, previousBattle.Id, battleFinished);
    }
}

