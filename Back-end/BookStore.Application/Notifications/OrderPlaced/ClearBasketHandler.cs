using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Notifications.OrderPlaced;
    
internal class ClearBasketHandler : INotificationHandler<OrderPlacedNotification>
{
    private ApplicationContext Context { get; }

    public ClearBasketHandler(ApplicationContext context)
    {
        Context = context;
    }

    public async Task Handle(OrderPlacedNotification notification, CancellationToken cancellationToken)
    {
        var orderUserId = await Context.Orders
            .Where(order => order.Id == notification.OrderId)
            .Select(order => order.UserId)
            .SingleAsync(cancellationToken);

        var basketProductsToRemove = await Context.BasketProducts
            .Where(basketProduct => basketProduct.UserId == orderUserId)
            .ToListAsync(cancellationToken);

        Context.RemoveRange(basketProductsToRemove);
        
        await Context.SaveChangesAsync();
    }
}

