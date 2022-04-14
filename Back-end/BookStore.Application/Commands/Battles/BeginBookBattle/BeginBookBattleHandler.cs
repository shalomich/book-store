using BookStore.Application.Services;
using BookStore.Domain.Entities.Battles;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Battles.BeginBookBattle;

public record BeginBookBattleCommand() : IRequest;
internal class BeginBookBattleHandler : AsyncRequestHandler<BeginBookBattleCommand>
{
    private ApplicationContext Context { get; }
    private BattleSettingsProvider BattleSettingsProvider { get; }

    public BeginBookBattleHandler(ApplicationContext context, BattleSettingsProvider battleSettingsProvider)
    {
        Context = context;
        BattleSettingsProvider = battleSettingsProvider;
    }

    protected override async Task Handle(BeginBookBattleCommand request, CancellationToken cancellationToken)
    {
        var currentBattle = await Context.Battles
            .SingleOrDefaultAsync(battle => battle.IsActive, cancellationToken);

        bool battleExist = currentBattle != null;
        
        if (battleExist)
        {
            bool battleFinished = currentBattle.EndDate < DateTimeOffset.UtcNow;

            if (!battleFinished)
            {
                return;
            }
            else
            {
                currentBattle.IsActive = false;
            }
        }

        var battleSettings = BattleSettingsProvider.GetBattleSettings();

        var newBattle = new Battle();
        
        newBattle.EndDate = DateTimeOffset.Now.AddDays(battleSettings.BattleDurationInDays);
        newBattle.BattleBooks = await FindBattleBooksAsync(cancellationToken);

        Context.Add(newBattle);

        await Context.SaveChangesAsync(cancellationToken);
    }

    private async Task<IEnumerable<BattleBook>> FindBattleBooksAsync(CancellationToken cancellationToken)
    {
        int bookIdForBattle = await Context.Books
            .Select(book => book.Id)
            .FirstAsync(cancellationToken);

        return new[]
        {
            new BattleBook { BookId = bookIdForBattle},
            new BattleBook { BookId = bookIdForBattle},
        };
    }
}

