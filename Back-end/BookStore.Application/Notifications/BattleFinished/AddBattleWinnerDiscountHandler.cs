using BookStore.Application.Services;
using BookStore.Domain.Entities.Battles;
using BookStore.Domain.Entities.Books;
using BookStore.Domain.Entities.Products;
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
           .Include(battleBook => battleBook.Book)
                .ThenInclude(book => book.Discount)
           .Select(battleBook => new
           {
               BattleBook = battleBook,
               VotingPointCount = battleBook.Votes
                   .Sum(vote => vote.VotingPointCount)
           })
           .ToListAsync(cancellationToken);

        var battleWinner = battleParticipants
            .MaxBy(battleParticipant => battleParticipant.VotingPointCount)
            .BattleBook.Book;

        int totalVotingPointCount = battleParticipants
            .Sum(battleParticipant => battleParticipant.VotingPointCount);

        var battleSettings = BattleSettingsProvider.GetBattleSettings();

        int currentDiscountPercentage = BattleCalculator.CalculateDiscount(totalVotingPointCount, battleSettings);

        var endDate = DateTimeOffset.Now.AddDays(battleSettings.BattleDurationInDays);

        battleWinner.Discount = new Discount
        {
            Percentage = currentDiscountPercentage,
            EndDate = endDate
        };

        Context.Books.Update(battleWinner);
        await Context.SaveChangesAsync(cancellationToken);
    }
 }

