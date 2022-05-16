using BookStore.Application.Exceptions;
using BookStore.Application.Services;
using BookStore.Domain.Enums;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Orders.CancelOrder;

public record CancelOrderCommand(int OrderId) : IRequest;
internal class CancelOrderHandler : AsyncRequestHandler<CancelOrderCommand>
{
    private ApplicationContext Context { get; }
    private LoggedUserAccessor LoggedUserAccessor { get; }

    public CancelOrderHandler(ApplicationContext context, LoggedUserAccessor loggedUserAccessor)
    {
        Context = context;
        LoggedUserAccessor = loggedUserAccessor;
    }

    protected override async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);

        var orderById = await Context.Orders
            .Include(order => order.Products)
            .ThenInclude(orderProduct => orderProduct.Product)
            .SingleAsync(order => order.Id == request.OrderId, cancellationToken);

        foreach (var orderProduct in orderById.Products)
        {
            orderProduct.Product.Quantity += orderProduct.Quantity;
        }

        orderById.State = OrderState.Cancelled;

        await Context.SaveChangesAsync(cancellationToken);
    }

    private async Task Validate(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        int currentUserId = LoggedUserAccessor.GetCurrentUserId();

        var orderById = await Context.Orders
            .Where(order => order.UserId == currentUserId)
            .SingleOrDefaultAsync(order => order.Id == request.OrderId, cancellationToken);

        if (orderById == null)
        {
            throw new NotFoundException("Current user has not order with this id.");
        }

        if (orderById.State != OrderState.Placed)
        {
            throw new BadRequestException($"Invalid order state ({orderById.State}), must be {OrderState.Placed}");
        }
    }
}

