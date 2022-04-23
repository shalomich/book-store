using BookStore.Persistance;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using BookStore.Domain.Entities;

namespace BookStore.Application.Notifications.OrderPlaced;
internal class DecreaseProductQuantityHandler : INotificationHandler<OrderPlacedNotification>
{
    private ApplicationContext Context { get; }

    public DecreaseProductQuantityHandler(ApplicationContext context)
    {
        Context = context;
    }

    public async Task Handle(OrderPlacedNotification notification, CancellationToken cancellationToken)
    {
        var orderProductCounts = await Context.Set<OrderProduct>()
            .Where(orderProduct => orderProduct.OrderId == notification.OrderId)
            .Select(orderProduct => new { orderProduct.ProductId, orderProduct.Quantity })
            .ToListAsync(cancellationToken);

        var productIds = orderProductCounts
            .Select(orderProductCount => orderProductCount.ProductId);

        var products = Context.Set<Product>()
            .Include(product => product.ProductCloseout)
            .Where(product => productIds.Contains(product.Id));

        foreach (var product in products)
        {
            var productDecrease = orderProductCounts
                .Single(orderProductCount => orderProductCount.ProductId == product.Id)
                .Quantity;

            product.Quantity -= productDecrease;

            if (product.Quantity == 0)
            {
                product.ProductCloseout = new ProductCloseout
                {
                    Date = DateTime.Now,
                };      
            }
        }

        await Context.SaveChangesAsync(cancellationToken);
    }
}

