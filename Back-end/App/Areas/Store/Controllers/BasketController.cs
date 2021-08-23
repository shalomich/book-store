using App.Areas.Store.ViewModels.Basket;
using App.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static App.Areas.Common.RequestHandlers.CreateHandler;
using static App.Areas.Common.RequestHandlers.DeleteHandler;
using static App.Areas.Common.RequestHandlers.GetEntityByIdHandler;
using static App.Areas.Common.RequestHandlers.UpdateHandler;

namespace App.Areas.Store.Controllers
{
    public class BasketController : UserController
    {
        private IMapper Mapper { get; }
        public BasketController(IMediator mediator, IMapper mapper) : base(mediator)
        {
            Mapper = mapper;
        }

        private async Task<Basket> GetUserBasket()
        {
            var user = await GetAuthorizedUser();

            var basket = user.Basket;

            return basket ?? (Basket) await Mediator.Send(new CreateCommand(new Basket() { User = user}));
        }

        [HttpGet]
        public async Task<ActionResult<BasketDto>> Get()
        {
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
            var basketProduct = (BasketProduct)await Mediator.Send(new GetByIdQuery(id, typeof(BasketProduct)));

            return Mapper.Map<BasketProductDto>(basketProduct);
        }

        [HttpPost("product")]
        public async Task<ActionResult<BasketProductDto>> AddBasketProduct(AddingBasketProduct addingProduct)
        {
            var product = (Product) await Mediator.Send(new GetByIdQuery(addingProduct.ProductId.Value, typeof(Product)));

            var basket = await GetUserBasket();

            var basketProduct = new BasketProduct { Basket = basket, Product = product };

            basketProduct = (BasketProduct) await Mediator.Send(new CreateCommand(basketProduct));

            return CreatedAtAction(nameof(GetBasketProduct), new { id = basketProduct.Id }, 
                Mapper.Map<BasketProductDto>(basketProduct));
        }

        [HttpPut("product/{id}")]
        public async Task<ActionResult<BasketProductDto>> ChangeBasketProductQuantity(int id, QuantityChangedBasketProduct quantityChangedBasketProduct)
        {
            var basketProduct = (BasketProduct) await Mediator.Send(new GetByIdQuery(id, typeof(BasketProduct)));
            
            basketProduct.Quantity = quantityChangedBasketProduct.Quantity;           

            await Mediator.Send(new UpdateCommand(id, basketProduct));

            return NoContent();
        }

        [HttpDelete("product/{id}")]
        public async Task<ActionResult<BasketProductDto>> DeleteBasketProduct(int id)
        {
            var basketProduct = (BasketProduct)await Mediator.Send(new GetByIdQuery(id, typeof(BasketProduct)));
            await Mediator.Send(new DeleteCommand(basketProduct));

            return NoContent();
        }
    }
}
