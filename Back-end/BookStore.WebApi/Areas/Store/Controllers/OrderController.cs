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

        [HttpGet("info")]
        public async Task<OrderUserInfo> GetOrderUserInfo()
        {
            return await Mediator.Send(new GetOrderUserInfoQuery());
        }

        [HttpGet]
        public async Task<IEnumerable<OrderDto>> GetOrders()
        {
            return await Mediator.Send(new GetOrdersQuery());
        }

        [HttpPost]
        public async Task PlaceOrder(OrderForm orderForm, [FromServices] DbEntityQueryBuilder<BasketProduct> basketProductQueryBuilder)
        {
            int userId = User.GetUserId();

            await Mediator.Send(new CheckBasketProductQuantiesQuery(userId));

            var orderMakingDto = Mapper.Map<OrderMakingDto>(orderForm);

            Order order = await Mediator.Send(new MakeOrderCommand(userId, orderMakingDto));
            
            await Mediator.Send(new CreateCommand(order));

            await Mediator.Publish(new OrderPlacedNotification(order));
        }
    }
}
