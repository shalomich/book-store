using BookStore.Application.Exceptions;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Basket.DeleteBasketProduct;

public record DeleteBasketProductCommand(int Id) : IRequest;
internal class DeleteBasketProductCommandHandler : AsyncRequestHandler<DeleteBasketProductCommand>
{
    public ApplicationContext Context { get; }
    public LoggedUserAccessor LoggedUserAccessor { get; }

    public DeleteBasketProductCommandHandler(
        ApplicationContext context,
        LoggedUserAccessor loggedUserAccessor)
    {
        Context = context;
        LoggedUserAccessor = loggedUserAccessor;
    }

    protected override async Task Handle(DeleteBasketProductCommand request, CancellationToken cancellationToken)
    {
        var basketProductById = await Context.BasketProducts
            .SingleOrDefaultAsync(basketProduct => basketProduct.Id == request.Id
                && basketProduct.UserId == LoggedUserAccessor.GetCurrentUserId(), cancellationToken);

        if (basketProductById == null)
        {
            throw new NotFoundException(nameof(BasketProduct));
        }

        Context.Remove(basketProductById);
        await Context.SaveChangesAsync(cancellationToken);
    }
}

