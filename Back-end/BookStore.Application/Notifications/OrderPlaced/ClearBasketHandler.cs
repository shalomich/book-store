using BookStore.Application.DbQueryConfigs.Specifications;
using BookStore.Domain.Entities;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Notifications.OrderPlaced
{
    
    internal class ClearBasketHandler : INotificationHandler<OrderPlacedNotification>
    {
        private ApplicationContext Context { get; }

        public ClearBasketHandler(ApplicationContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Handle(OrderPlacedNotification notification, CancellationToken cancellationToken)
        {
            var user = (User) await Context.Users
                .Include(user => user.BasketProducts)
                .FirstOrDefaultAsync(new EntityByIdSpecification(notification.Order.UserId).ToExpression());

            user.BasketProducts = null;

            await Context.SaveChangesAsync();
        }
    }
}
