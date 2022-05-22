using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BookStore.Application.Commands.Basket.AddProductToBasket;
using System.Threading;
using BookStore.Application.Commands.Basket.GetBasketProducts;
using BookStore.Application.Commands.Basket.ChangeBasketProductQuantity;
using BookStore.Application.Commands.Basket.CleanBasket;
using BookStore.Application.Commands.Basket.DeleteBasketProduct;

namespace BookStore.WebApi.Areas.Store.Controllers;

[Route("[area]/[controller]/product")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class BasketController : StoreController
{
    private IMediator Mediator { get; }
    
    public BasketController(
        IMediator mediator)
    {
        Mediator = mediator;
    }

    [HttpGet]
    public async Task<IEnumerable<BasketProductDto>> GetBasketProducts(CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetBasketProductsQuery(), cancellationToken);
    }

    [HttpPost]
    public async Task<NoContentResult> AddProductToBasket([FromBody] AddProductToBasketDto productDto, CancellationToken cancellationToken)
    {
        await Mediator.Send(new AddProductToBasketCommand(productDto), cancellationToken);
            
        return NoContent();
    }

    [HttpPut]
    public async Task<NoContentResult> ChangeBasketProductQuantity([FromBody] ChangeBasketProductQuantityDto productDto, CancellationToken cancellationToken)
    {
        await Mediator.Send(new ChangeBasketProductQuantityCommand(productDto), cancellationToken);

        return NoContent();
    }

    [HttpDelete]
    public async Task<NoContentResult> CleanBasket(CancellationToken cancellationToken)
    {
        await Mediator.Send(new CleanBasketCommand(), cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<NoContentResult> DeleteBasketProduct(int id, CancellationToken cancellationToken)
    {
        await Mediator.Send(new DeleteBasketProductCommand(id), cancellationToken);

        return NoContent();
    }
}

