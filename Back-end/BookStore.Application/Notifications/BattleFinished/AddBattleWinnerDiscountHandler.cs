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
internal class AddBattleWinnerDiscountHandler : INotificationHandler<BattleFinishedNotification>
{
    private ApplicationContext Context { get; }
    private BattleSettingsProvider BattleSettingsProvider { get; }

    public AddBattleWinnerDiscountHandler(ApplicationContext context, BattleSettingsProvider battleSettingsProvider)
    {
        Context = context;
        BattleSettingsProvider = battleSettingsProvider;
    }

    public async Task Handle(BattleFinishedNotification notification, CancellationToken cancellationToken)
    {
        var battleParticipants = await Context.Set<BattleBook>()
           .Where(battleBook => battleBook.BattleId == notification.BattleId)
           .Select(battleBook => new
           {
               battleBook.Book,
               VotingPointCount = battleBook.Votes
                   .Sum(vote => vote.VotingPointCount)
           })
           .ToListAsync(cancellationToken);

        var battleWinner = battleParticipants
            .MaxBy(battleParticipant => battleParticipant.VotingPointCount)
            .Book;

        int totalVotingPointCount = battleParticipants
            .Sum(battleParticipant => battleParticipant.VotingPointCount);

        int currentDiscount = BattleCalculator.CalculateDiscount(totalVotingPointCount, BattleSettingsProvider.GetBattleSettings());

        battleWinner.DiscountPercentage = currentDiscount;

        Context.Books.Update(battleWinner);
        await Context.SaveChangesAsync(cancellationToken);
    }
 }

