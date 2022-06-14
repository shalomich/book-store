using BookStore.Application.Exceptions;
using BookStore.Application.Services;
using BookStore.Domain.Enums;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Orders.MarkAsDelivered;

public record MarkAsDeliveredCommand(int OrderId) : IRequest;
internal class MarkAsDeliveredHandler : AsyncRequestHandler<MarkAsDeliveredCommand>
{
    private ApplicationContext Context { get; }
    private LoggedUserAccessor LoggedUserAccessor { get; }

    public MarkAsDeliveredHandler(ApplicationContext context, LoggedUserAccessor loggedUserAccessor)
    {
        Context = context;
        LoggedUserAccessor = loggedUserAccessor;
    }

    protected override async Task Handle(MarkAsDeliveredCommand request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);

        var orderById = await Context.Orders
            .SingleAsync(order => order.Id == request.OrderId, cancellationToken);

        orderById.State = OrderState.Delivered;
        orderById.DeliveredDate = DateTimeOffset.Now;

        await Context.SaveChangesAsync(cancellationToken);
    }

    private async Task Validate(MarkAsDeliveredCommand request, CancellationToken cancellationToken)
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

