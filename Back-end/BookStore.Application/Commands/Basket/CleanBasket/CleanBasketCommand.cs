using BookStore.Application.Services;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Basket.CleanBasket;

public record CleanBasketCommand() : IRequest;
internal class CleanBasketCommandHandler : AsyncRequestHandler<CleanBasketCommand>
{
    public ApplicationContext Context { get; }
    public LoggedUserAccessor LoggedUserAccessor { get; }

    public CleanBasketCommandHandler(
        ApplicationContext context,
        LoggedUserAccessor loggedUserAccessor)
    {
        Context = context;
        LoggedUserAccessor = loggedUserAccessor;
    }

    protected override async Task Handle(CleanBasketCommand request, CancellationToken cancellationToken)
    {
        var userById = await Context.Users
            .Include(user => user.BasketProducts)
            .SingleAsync(user => user.Id == LoggedUserAccessor.GetCurrentUserId());

        userById.BasketProducts = null;

        await Context.SaveChangesAsync(cancellationToken);
    }
}

