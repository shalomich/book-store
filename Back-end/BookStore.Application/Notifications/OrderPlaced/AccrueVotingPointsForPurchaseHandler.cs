using BookStore.Application.Services;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Notifications.OrderPlaced;

internal class AccrueVotingPointsForPurchaseHandler : INotificationHandler<OrderPlacedNotification>
{
    private ApplicationContext Context { get; }
    private BattleSettingsProvider BattleSettingsProvider { get; }

    public AccrueVotingPointsForPurchaseHandler(ApplicationContext context, BattleSettingsProvider battleSettingsProvider)
    {
        Context = context;
        BattleSettingsProvider = battleSettingsProvider;
    }

    public async Task Handle(OrderPlacedNotification notification, CancellationToken cancellationToken)
    {
        var order = notification.Order;

        var totalAmount = order.Products
            .Sum(orderProduct => orderProduct.Cost * orderProduct.Quantity);

        var battleSettings = BattleSettingsProvider.GetBattleSettings();

        var votingPointCount = (int) Math.Round(totalAmount * battleSettings.VotingPointCountPerRuble);

        var user = await Context.Users
            .SingleAsync(user => user.Id == order.UserId, cancellationToken);

        user.VotingPointCount += votingPointCount;

        await Context.SaveChangesAsync(cancellationToken);
    }
}

