using AutoMapper;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Domain.Entities;
using BookStore.WebApi.Areas.Store.ViewModels.Basket;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BookStore.WebApi.Extensions;
using BookStore.Application.DbQueryConfigs.Specifications;
using BookStore.Application.DbQueryConfigs.IncludeRequirements;
using BookStore.Application.Queries;
using System.Linq;
using BookStore.Application.Commands;
using BookStore.Application.Dto;
using BookStore.Application.Notifications.OrderPlaced;
using System.Collections.Generic;
using BookStore.Application.Queries.Order.GetOrders;
using BookStore.Application.Commands.Orders.PlaceOrder;
using System.Threading;
using BookStore.Application.Commands.Orders.MarkAsDelivered;

namespace BookStore.WebApi.Areas.Store.Controllers
{
    [Route("[area]/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class OrderController : StoreController
    {
        private IMediator Mediator { get; }
        private IMapper Mapper { get; }
        
        public OrderController(IMediator mediator, IMapper mapper)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IEnumerable<OrderDto>> GetOrders()
        {
            return await Mediator.Send(new GetOrdersQuery());
        }

        [HttpPost]
        public async Task<int> PlaceOrder(OrderForm orderForm, CancellationToken cancellationToken)
        {
            var orderId = await Mediator.Send(new PlaceOrderCommand(orderForm), cancellationToken);

            await Mediator.Publish(new OrderPlacedNotification(orderId), cancellationToken);

            return orderId;
        }

        [HttpPut("{orderId}/delivered")]
        public async Task MarkAsDelivered(int orderId, CancellationToken cancellationToken)
        {
            await Mediator.Send(new MarkAsDeliveredCommand(orderId), cancellationToken);
        }
    }
}
