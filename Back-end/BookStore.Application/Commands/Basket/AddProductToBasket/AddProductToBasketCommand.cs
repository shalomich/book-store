using BookStore.Application.Exceptions;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using BookStore.Domain.Entities.Books;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Basket.AddProductToBasket;

public record AddProductToBasketCommand(AddProductToBasketDto ProductDto) : IRequest;
internal class AddProductToBasketCommandHandler : AsyncRequestHandler<AddProductToBasketCommand>
{
    public ApplicationContext Context { get; }
    public LoggedUserAccessor LoggedUserAccessor { get; }

    public AddProductToBasketCommandHandler(
        ApplicationContext context,
        LoggedUserAccessor loggedUserAccessor)
    {
        Context = context;
        LoggedUserAccessor = loggedUserAccessor;
    }

    protected override async Task Handle(AddProductToBasketCommand request, CancellationToken cancellationToken)
    {
        var productDto = request.ProductDto;

        var bookById = await Context.Books
            .SingleOrDefaultAsync(book => book.Id == productDto.ProductId, cancellationToken);

        if (bookById == null)
        {
            throw new NotFoundException(nameof(Book));
        }

        if (bookById.Quantity == 0)
        {
            throw new BadRequestException("Book quantity equal zero.");
        }

        var basketProduct = new BasketProduct
        {
            ProductId = bookById.Id,
            UserId = LoggedUserAccessor.GetCurrentUserId()
        };

        await Context.BasketProducts.AddAsync(basketProduct, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
    }
}

