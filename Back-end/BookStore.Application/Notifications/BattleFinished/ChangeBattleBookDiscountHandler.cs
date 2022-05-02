using BookStore.Application.Queries.Battle.GetBattleSettings;
using BookStore.Application.Services;
using BookStore.Domain.Entities.Battles;
using BookStore.Domain.Entities.Books;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Notifications.BattleFinished;
internal class ChangeBattleBookDiscountHandler : INotificationHandler<BattleFinishedNotification>
{
    private ApplicationContext Context { get; }
    private BattleSettingsProvider BattleSettingsProvider { get; }

    public ChangeBattleBookDiscountHandler(ApplicationContext context, BattleSettingsProvider battleSettingsProvider)
    {
        Context = context;
        BattleSettingsProvider = battleSettingsProvider;
    }

    public async Task Handle(BattleFinishedNotification notification, CancellationToken cancellationToken)
    {
        var (currentBattleId, previousBattleId) = notification;

        var previousBattleWinner = await FindBattleWinner(previousBattleId, cancellationToken);
        var currentBattleWinner = await FindBattleWinner(currentBattleId, cancellationToken);

        int totalVotingPointCount = await GetCurrentBattleTotalVotingPointCount(currentBattleId, cancellationToken);

        int currentDiscount = BattleCalculator.CalculateDiscount(totalVotingPointCount, BattleSettingsProvider.GetBattleSettings());

        previousBattleWinner.DiscountPercentage = 0;
        currentBattleWinner.DiscountPercentage = currentDiscount;

        await Context.SaveChangesAsync(cancellationToken);
    }

    private async Task<Book> FindBattleWinner(int battleId, CancellationToken cancellationToken)
    {
        return await Context.Set<BattleBook>()
           .Where(battleBook => battleBook.BattleId == battleId)
           .OrderByDescending(battleBook => battleBook.Votes
               .Select(vote => vote.VotingPointCount)
               .Sum())
           .Select(battleBook => battleBook.Book)
           .FirstAsync(cancellationToken);
    }

    private async Task<int> GetCurrentBattleTotalVotingPointCount(int currentBattleId, CancellationToken cancellationToken)
    {
        return await Context.Votes
            .Where(vote => vote.BattleBook.BattleId == currentBattleId)
            .SumAsync(vote => vote.VotingPointCount, cancellationToken);
    }
 }

