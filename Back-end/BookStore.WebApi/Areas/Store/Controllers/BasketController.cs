
using BookStore.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Entities.Products;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Application.DbQueryConfigs.IncludeRequirements;
using BookStore.Application.Commands;
using BookStore.Application.Queries;
using BookStore.Application.Dto;

namespace BookStore.WebApi.Areas.Store.Controllers
{
    public class BasketController : UserController
    {
        private IMapper Mapper { get; }
        private DbEntityQueryBuilder<BasketProduct> BasketProductQueryBuilder { get; }
        private DbEntityQueryBuilder<Basket> BasketQueryBuilder { get; }
        public BasketController(IMediator mediator, IMapper mapper, DbEntityQueryBuilder<User> userQueryBuilder, 
            DbEntityQueryBuilder<BasketProduct> basketProductQueryBuilder, 
            DbEntityQueryBuilder<Basket> basketQueryBuilder) : 
            base(mediator, userQueryBuilder)
        {
            Mapper = mapper;
            BasketProductQueryBuilder = basketProductQueryBuilder;
            BasketQueryBuilder = basketQueryBuilder;
        }

        private async Task<Basket> GetUserBasket()
        {
            UserQueryBuilder.AddIncludeRequirements(new UserBasketIncludeRequirement());
            var user = await GetAuthorizedUser(UserQueryBuilder);

            var basket = user.Basket;

            if (basket == null)
                basket = (Basket)await Mediator.Send(new CreateCommand(user.CreateBasket()));
            
            return basket; 
        }

        [HttpGet]
        public async Task<ActionResult<BasketDto>> Get()
        {
            BasketQueryBuilder.AddIncludeRequirements(new BasketProductIncludeRequirement());

            var basket = await GetUserBasket();

            return Mapper.Map<BasketDto>(basket);
        }

        [HttpDelete]
        public async Task<ActionResult<BasketDto>> Delete()
        {
            var basket = await GetUserBasket();

            await Mediator.Send(new DeleteCommand(basket));

            return NoContent();
        }

        [HttpGet("product/{id}")]
        public async Task<ActionResult<BasketProductDto>> GetBasketProduct(int id)
        {
            var basketProduct = (BasketProduct)await Mediator.Send(new GetByIdQuery(id, BasketProductQueryBuilder));

            return Mapper.Map<BasketProductDto>(basketProduct);
        }

        [HttpPost("product")]
        public async Task<ActionResult<BasketProductDto>> AddBasketProduct([FromBody] int productId, [FromServices] DbEntityQueryBuilder<Product> productQueryBuilder)
        {
            var product = (Product) await Mediator.Send(new GetByIdQuery(productId, productQueryBuilder));

            var basket = await GetUserBasket();

            var basketProduct = new BasketProduct { Basket = basket, Product = product };

            basketProduct = (BasketProduct) await Mediator.Send(new CreateCommand(basketProduct));

            return CreatedAtAction(nameof(GetBasketProduct), new { id = basketProduct.Id }, 
                Mapper.Map<BasketProductDto>(basketProduct));
        }

        [HttpPut("product/{id}")]
        public async Task<ActionResult<BasketProductDto>> ChangeBasketProductQuantity(int id, [FromBody][Range(1,int.MaxValue)] int quantity)
        {
            var basketProduct = (BasketProduct) await Mediator.Send(new GetByIdQuery(id, BasketProductQueryBuilder));

            basketProduct.Quantity = quantity;

            await Mediator.Send(new UpdateCommand(id, basketProduct));

            return NoContent();
        }

        [HttpDelete("product/{id}")]
        public async Task<ActionResult<BasketProductDto>> DeleteBasketProduct(int id)
        {
            var basketProduct = (BasketProduct)await Mediator.Send(new GetByIdQuery(id, BasketProductQueryBuilder));
            await Mediator.Send(new DeleteCommand(basketProduct));

            return NoContent();
        }
    }
}
