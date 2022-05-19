using BookStore.Application.Exceptions;
using BookStore.Application.Queries.Battle.GetBattleSettings;
using BookStore.Application.Services;
using BookStore.Application.Services.CatalogSelections;
using BookStore.Domain.Entities.Battles;
using BookStore.Domain.Entities.Books;
using BookStore.Domain.Enums;
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
    private BooksForBattleSelection BooksForBattleSelection { get; }

    public StartBattleHandler(ApplicationContext context, BattleSettingsProvider battleSettingsProvider,
        BooksForBattleSelection booksForBattleSelection)
    {
        Context = context;
        BattleSettingsProvider = battleSettingsProvider;
        BooksForBattleSelection = booksForBattleSelection;
    }

    public async Task<StartBattleResult> Handle(StartBattleCommand request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);

        var battleSettings = BattleSettingsProvider.GetBattleSettings();

        var newBattle = await CreateNewBattle(battleSettings, cancellationToken);

        Context.Add(newBattle);

        await Context.SaveChangesAsync(cancellationToken);

        return new StartBattleResult(newBattle.Id, newBattle.EndDate);
    }

    private async Task<Battle> CreateNewBattle(BattleSettings battleSettings, CancellationToken cancellationToken)
    {
        var booksForBattle = await FindBooksForBattleAsync(battleSettings, cancellationToken);

        var endDate = DateTimeOffset.Now.AddDays(battleSettings.BattleDurationInDays);

        var newBattle = new Battle()
        {
            BattleBooks = booksForBattle
                .Select(book => new BattleBook
                {
                    BookId = book.Id
                })
                .ToList(),
            EndDate = endDate
        };

        return newBattle;
    }

    private async Task<IEnumerable<Book>> FindBooksForBattleAsync(BattleSettings battleSettings, CancellationToken cancellationToken)
    { 
        var booksForBattle = BooksForBattleSelection.Select();

        var currentBattleBooks = await booksForBattle
            .SelectMany(firstBook => booksForBattle
                .Select(secondBook => new
                {
                    First = firstBook,
                    Second = secondBook,
                    CostDifference = firstBook.Cost - secondBook.Cost
                }))
            .Where(books => (books.CostDifference >= 0 && books.CostDifference <= battleSettings.MaxBookCostDifference)
                || (books.CostDifference < 0 && books.CostDifference >= -battleSettings.MaxBookCostDifference))
            .FirstOrDefaultAsync(cancellationToken);

        if (currentBattleBooks == null)
        {
            throw new InvalidOperationException("There is no valid book pair for battle.");
        }

        return new Book[]
        {
            currentBattleBooks.First,
            currentBattleBooks.Second
        };
    }

    private async Task Validate(StartBattleCommand request, CancellationToken cancellationToken)
    {
        bool hasNotFinishedBattles = await Context.Battles
            .Where(battle => battle.State != BattleState.Finished)
            .AnyAsync(cancellationToken);

        if (hasNotFinishedBattles)
        {
            throw new BadRequestException("There is battle which not finished yet.");
        }

        var battleSettings = BattleSettingsProvider.GetBattleSettings();

        var previousBooksForBattle = Context.Set<BattleBook>()
            .Select(battleBook => battleBook.Book);

        var notBattleBooks = Context.Books
            .Except(previousBooksForBattle);

        bool hasBooksWithCostMoreLowerBound = await notBattleBooks
            .Where(book => book.Cost >= battleSettings.LowerBoundBookCost)
            .AnyAsync(cancellationToken);

        if (!hasBooksWithCostMoreLowerBound)
        {
            throw new InvalidOperationException("There are not book that have cost more that lower bound.");
        }

        bool hasBookWithOrderDelivered = await notBattleBooks
            .Where(book => book.OrderProducts
                .Any(orderProduct => orderProduct.Order.State == OrderState.Delivered))
            .AnyAsync(cancellationToken);

        if (!hasBookWithOrderDelivered)
        {
            throw new InvalidOperationException("There are not book that order delivered.");
        }
    }
}

