using BookStore.Application.Services;
using BookStore.Domain.Entities;
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
        var totalAmount = await Context.Set<OrderProduct>()
            .Where(orderProduct => orderProduct.OrderId == notification.OrderId)
            .SumAsync(orderProduct => orderProduct.Cost * orderProduct.Quantity, cancellationToken);

        var battleSettings = BattleSettingsProvider.GetBattleSettings();

        var votingPointCount = (int) Math.Round(totalAmount * battleSettings.VotingPointCountPerRuble);

        var user = await Context.Users
            .Where(user => user.Orders.Any(order => order.Id == notification.OrderId))
            .SingleAsync(cancellationToken);

        user.VotingPointCount += votingPointCount;

        await Context.SaveChangesAsync(cancellationToken);
    }
}

