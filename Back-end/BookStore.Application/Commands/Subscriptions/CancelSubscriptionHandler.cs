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

public record CancelSubscriptionCommand() : IRequest;

internal class CancelSubscriptionHandler : AsyncRequestHandler<CancelSubscriptionCommand>
{
    private ApplicationContext Context { get; }
    private LoggedUserAccessor LoggedUserAccessor { get; }

    public CancelSubscriptionHandler(ApplicationContext context, LoggedUserAccessor loggedUserAccessor)
    {
        Context = context;
        LoggedUserAccessor = loggedUserAccessor;
    }

    protected override async Task Handle(CancelSubscriptionCommand request, CancellationToken cancellationToken)
    {
        int userId = LoggedUserAccessor.GetCurrentUserId();

        var userById = await Context.Users
            .Include(user => user.Subscription)
            .SingleAsync(user => user.Id == userId, cancellationToken);

        var subscriptionByUserId = await Context.Set<Subscription>()
            .SingleOrDefaultAsync(subscription => subscription.UserId == userId,
                cancellationToken);

        if (subscriptionByUserId == null)
        {
            throw new BadRequestException("Subscription is not activated.");
        }

        Context.Remove(subscriptionByUserId);

        await Context.SaveChangesAsync(cancellationToken);
    }
}

