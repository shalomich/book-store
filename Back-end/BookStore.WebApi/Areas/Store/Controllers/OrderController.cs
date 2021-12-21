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

        [HttpPost]
        public async Task PlaceOrder(OrderForm orderForm, [FromServices] DbEntityQueryBuilder<BasketProduct> basketProductQueryBuilder)
        {
            var order = Mapper.Map<Order>(orderForm);

            int userId = User.GetUserId();
            order.UserId = userId;

            basketProductQueryBuilder
                .AddSpecification(new BasketProductByUserIdSpecification(userId))
                .AddIncludeRequirements(new BasketProductIncludeRequirement());
            
            var basketProducts = await Mediator.Send(new GetQuery(basketProductQueryBuilder));

            var orderProducts = basketProducts
                .Select(basketProduct => Mapper.Map<OrderProduct>(basketProduct))
                .ToHashSet();

            order.Products = orderProducts;

            await Mediator.Send(new CreateCommand(order));
        }
    }
}
