using BookStore.Application.Queries.Battle.GetBattleSettings;
using BookStore.Application.Services;
using BookStore.Domain.Entities.Battles;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Notifications.BattleFinished;
internal class StartNewBattleHandler : INotificationHandler<BattleFinishedNotification>
{
    private ApplicationContext Context { get; }
    private BattleSettingsProvider BattleSettingsProvider { get; }

    public StartNewBattleHandler(ApplicationContext context, BattleSettingsProvider battleSettingsProvider)
    {
        Context = context;
        BattleSettingsProvider = battleSettingsProvider;
    }

    public async Task Handle(BattleFinishedNotification request, CancellationToken cancellationToken)
    {
        var currentBattle = await Context.Battles
            .Where(battle => battle.IsActive)
            .SingleAsync(battle => battle.Id == request.CurrentBattleId, cancellationToken);

        currentBattle.IsActive = false;

        var battleSettings = BattleSettingsProvider.GetBattleSettings();

        var newBattle = await CreateNewBattle(battleSettings, cancellationToken);

        Context.Add(newBattle);
        await Context.SaveChangesAsync(cancellationToken);
    }

    private async Task<Battle> CreateNewBattle(BattleSettings battleSettings, CancellationToken cancellationToken)
    {
        var newBattle = new Battle();

        newBattle.EndDate = DateTimeOffset.Now.AddDays(battleSettings.BattleDurationInDays);
        newBattle.BattleBooks = await FindBattleBooksAsync(cancellationToken);

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

