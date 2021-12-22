using BookStore.Persistance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Domain.Entities.Products;

namespace BookStore.Application.Notifications.OrderPlaced
{
    internal class DecreaseProductQuantityHandler : INotificationHandler<OrderPlacedNotification>
    {
        private ApplicationContext Context { get; }

        public DecreaseProductQuantityHandler(ApplicationContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Handle(OrderPlacedNotification notification, CancellationToken cancellationToken)
        {
            var productQuantityDecreases = notification.Order.Products
                .ToDictionary(orderProduct => orderProduct.ProductId, orderProduct => orderProduct.Quantity);

            int[] productIds = productQuantityDecreases.Keys.ToArray();

            var products = Context.Set<Product>()
                .Where(product => productIds.Contains(product.Id));

            foreach (var product in products)
                product.Quantity -= productQuantityDecreases[product.Id];
            
            await Context.SaveChangesAsync();
        }
    }
}
