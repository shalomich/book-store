using BookStore.Application.Queries.Battle.GetBattleSettings;
using BookStore.Application.Services;
using BookStore.Domain.Entities.Battles;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Battles.StartBattle;

public record StartBattleCommand() : IRequest<StartBattleResult>;
internal class StartBattleHandler : IRequestHandler<StartBattleCommand, StartBattleResult>
{
    private ApplicationContext Context { get; }
    private BattleSettingsProvider BattleSettingsProvider { get; }

    public StartBattleHandler(ApplicationContext context, BattleSettingsProvider battleSettingsProvider)
    {
        Context = context;
        BattleSettingsProvider = battleSettingsProvider;
    }

    public async Task<StartBattleResult> Handle(StartBattleCommand request, CancellationToken cancellationToken)
    {
        var battleSettings = BattleSettingsProvider.GetBattleSettings();

        var newBattle = await CreateNewBattle(battleSettings, cancellationToken);

        Context.Add(newBattle);

        await Context.SaveChangesAsync(cancellationToken);

        return new StartBattleResult(newBattle.Id, newBattle.EndDate);
    }

    private async Task<Battle> CreateNewBattle(BattleSettings battleSettings, CancellationToken cancellationToken)
    {
        var battleBooks = await FindBattleBooksAsync(cancellationToken);

        var endDate = DateTimeOffset.Now.AddDays(battleSettings.BattleDurationInDays);

        var newBattle = new Battle()
        {
            BattleBooks = battleBooks,
            EndDate = endDate
        };

        return newBattle;
    }

    private async Task<IEnumerable<BattleBook>> FindBattleBooksAsync(CancellationToken cancellationToken)
    {
        var bookIdsForBattle = await Context.Books
            .Select(book => book.Id)
            .Take(2)
            .ToListAsync(cancellationToken);

        return new[]
        {
            new BattleBook { BookId = bookIdsForBattle.First()},
            new BattleBook { BookId = bookIdsForBattle.Last()},
        };
    }
}

