using BookStore.Application.Exceptions;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Subscriptions;

public record ActivateSubscriptionCommand(string PhoneNumber) : IRequest;

internal class ActivateSubscriptionHandler : AsyncRequestHandler<ActivateSubscriptionCommand>
{
    private ApplicationContext Context { get; }
    private LoggedUserAccessor LoggedUserAccessor { get; }

    public ActivateSubscriptionHandler(ApplicationContext context, LoggedUserAccessor loggedUserAccessor)
    {
        Context = context;
        LoggedUserAccessor = loggedUserAccessor;
    }

    protected override async Task Handle(ActivateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        int userId = LoggedUserAccessor.GetCurrentUserId();

        var userById = await Context.Users
            .Include(user => user.Subscription)
            .SingleAsync(user => user.Id == userId, cancellationToken);

        if (userById.Subscription != null)
        {
            throw new BadRequestException("Subscription has already activated.");
        }

        if (userById.PhoneNumber != request.PhoneNumber)
        {
            userById.PhoneNumber = request.PhoneNumber;
        }

        userById.Subscription = new Subscription();

        await Context.SaveChangesAsync(cancellationToken);
    }
}

