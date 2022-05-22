using BookStore.Application.Exceptions;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Basket.ChangeBasketProductQuantity;

public record ChangeBasketProductQuantityCommand(ChangeBasketProductQuantityDto ProductDto) : IRequest;
internal class ChangeBasketProductQuantityCommandHandler : AsyncRequestHandler<ChangeBasketProductQuantityCommand>
{
    public ApplicationContext Context { get; }
    public LoggedUserAccessor LoggedUserAccessor { get; }

    public ChangeBasketProductQuantityCommandHandler(
        ApplicationContext context,
        LoggedUserAccessor loggedUserAccessor)
    {
        Context = context;
        LoggedUserAccessor = loggedUserAccessor;
    }

    protected override async Task Handle(ChangeBasketProductQuantityCommand request, CancellationToken cancellationToken)
    {
        var productDto = request.ProductDto;

        var basketProductById = await Context.BasketProducts
            .SingleOrDefaultAsync(basketProduct => basketProduct.Id == productDto.Id
                && basketProduct.UserId == LoggedUserAccessor.GetCurrentUserId(), cancellationToken);

        if (basketProductById == null)
        {
            throw new NotFoundException(nameof(BasketProduct));
        }

        var productQuantity = await Context.Books
            .Where(book => book.Id == basketProductById.ProductId)
            .Select(book => book.Quantity)
            .SingleAsync(cancellationToken);

        if (productDto.Quantity > productQuantity)
        {
            throw new BadRequestException($"Product quantity is not enough: {productQuantity}");
        }

        basketProductById.Quantity = productDto.Quantity;

        await Context.SaveChangesAsync(cancellationToken);
    }
}

