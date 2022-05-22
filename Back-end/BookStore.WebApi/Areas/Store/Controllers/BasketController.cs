
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
using BookStore.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using BookStore.Application.DbQueryConfigs.Specifications;
using BookStore.Application.Exceptions;
using BookStore.WebApi.Areas.Store.ViewModels.Basket;
using BookStore.Application.Commands.RelatedEntityEditing.CreateRelatedEntity;

namespace BookStore.WebApi.Areas.Store.Controllers
{
    [Route("[area]/[controller]/product")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class BasketController : StoreController
    {
        private IMediator Mediator { get; }
        private IMapper Mapper { get; }
        private DbEntityQueryBuilder<BasketProduct> BasketProductQueryBuilder { get; }

        public BasketController(IMediator mediator, IMapper mapper, DbEntityQueryBuilder<BasketProduct> basketProductQueryBuilder)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            BasketProductQueryBuilder = basketProductQueryBuilder ?? throw new ArgumentNullException(nameof(basketProductQueryBuilder));
        }

        [HttpGet]
        public async Task<IEnumerable<BasketProductDto>> GetBasketProducts()
        {
            return await Mediator.Send(new GetBasketProductsQuery());
        }

        [HttpPost]
        public async Task<ActionResult<BasketProductView>> AddBasketProduct([FromBody] BasketProductAddView addedBasketProduct, [FromServices] DbEntityQueryBuilder<Product> productQueryBuilder)
        {
            /*
            var product = (Product) await Mediator.Send(new GetByIdQuery(addedBasketProduct.ProductId.Value, productQueryBuilder));

            var basketProduct = new BasketProduct { ProductId = product.Id, UserId = User.GetUserId()};

            await Mediator.Send(new CheckBasketProductQuantityQuery(basketProduct));

            await Mediator.Send(new CreateRelatedEntityCommand(basketProduct));
            */
            return NoContent();
        }

        private async Task<BasketProduct> GetBasketProductById(int id)
        {
            /*
            var basketProduct = (BasketProduct) await Mediator.Send(new GetByIdQuery(id, BasketProductQueryBuilder));

            if (basketProduct.UserId != User.GetUserId())
                throw new BadRequestException("This basket product does not belong to authorized user");

            return basketProduct;*/

            return null;
        }

        [HttpPut]
        public async Task<ActionResult<BasketProductView>> ChangeBasketProductQuantity([FromBody] BasketProductUpdateView updateBasketProduct, [FromServices] DbEntityQueryBuilder<Product> productQueryBuilder)
        {
            var (id, quantity) = updateBasketProduct;

            var basketProduct = await GetBasketProductById(id);

            basketProduct.Quantity = quantity;

            await Mediator.Send(new CheckBasketProductQuantityQuery(basketProduct));
            
            //await Mediator.Send(new UpdateCommand(id, basketProduct));

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult<BasketProductView>> DeleteAllBasketProducts()
        {
            BasketProductQueryBuilder
                .AddSpecification(new BasketProductByUserIdSpecification(User.GetUserId()));

            var basketProducts = await Mediator.Send(new GetQuery(BasketProductQueryBuilder));

            foreach (var basketProduct in basketProducts)
                await Mediator.Send(new DeleteCommand(basketProduct));

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BasketProductView>> DeleteBasketProduct(int id)
        {
            var basketProduct = await GetBasketProductById(id);
            
            await Mediator.Send(new DeleteCommand(basketProduct));

            return NoContent();
        }
    }
}
