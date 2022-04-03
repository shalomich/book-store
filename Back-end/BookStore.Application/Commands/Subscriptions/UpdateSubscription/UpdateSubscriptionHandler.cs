using BookStore.Application.DbQueryConfigs.Specifications;
using BookStore.Application.Exceptions;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Subscriptions.UpdateSubscription;

public record UpdateSubscriptionCommand(UpdateSubscriptionDto SubscriptionDto) : IRequest;

internal class UpdateSubscriptionHandler : AsyncRequestHandler<UpdateSubscriptionCommand>
{
    private ApplicationContext Context { get; }
    private LoggedUserAccessor LoggedUserAccessor { get; }

    public UpdateSubscriptionHandler(ApplicationContext context, LoggedUserAccessor loggedUserAccessor)
    {
        Context = context;
        LoggedUserAccessor = loggedUserAccessor;
    }

    protected override async Task Handle(UpdateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscriptionDto = request.SubscriptionDto;

        int userId = LoggedUserAccessor.GetCurrentUserId();

        var subscriptionByUserId = await Context.Set<Subscription>()
            .SingleOrDefaultAsync(subscription => subscription.UserId == userId, 
                cancellationToken);

        if (subscriptionByUserId == null)
        {
            throw new BadRequestException("Subscription is not activated.");
        }

        subscriptionByUserId.TagNotificationEnable = subscriptionDto.TagNotificationEnable.Value;
        subscriptionByUserId.MarkNotificationEnable = subscriptionDto.MarkNotificationEnable.Value;

        await Context.SaveChangesAsync(cancellationToken);
    }
}
