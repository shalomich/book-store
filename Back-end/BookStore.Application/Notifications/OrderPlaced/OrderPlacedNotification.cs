using BookStore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Notifications.OrderPlaced
{
    public record OrderPlacedNotification(Order Order) : INotification;
}
