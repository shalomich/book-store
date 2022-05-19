using AutoMapper;
using BookStore.Application.Notifications.PhoneNumberUpdated;
using BookStore.Application.Services;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.UserProfile.UpdateUserProfile;

public record UpdateUserProfileCommand(UserProfileForm ProfileForm) : IRequest;
internal class UpdateUserProfileCommandHandler : AsyncRequestHandler<UpdateUserProfileCommand>
{
    private ApplicationContext Context { get; }
    private IMediator Mediator { get; }
    private IMapper Mapper { get; }
    private LoggedUserAccessor LoggedUserAccessor { get; }

    public UpdateUserProfileCommandHandler(
        ApplicationContext context, 
        IMediator mediator, 
        IMapper mapper,
        LoggedUserAccessor loggedUserAccessor)
    {
        Context = context;
        Mediator = mediator;
        Mapper = mapper;
        LoggedUserAccessor = loggedUserAccessor;
    }

    protected override async Task Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = LoggedUserAccessor.GetCurrentUserId();

        var currentUser = await Context.Users
            .SingleAsync(user => user.Id == currentUserId, cancellationToken);

        var previousPhoneNumber = currentUser.PhoneNumber;

        currentUser = Mapper.Map(request.ProfileForm, currentUser);

        await Context.SaveChangesAsync(cancellationToken);

        if (currentUser.PhoneNumber != previousPhoneNumber)
        {
            await Mediator.Publish(new PhoneNumberUpdatedNotification(currentUserId), cancellationToken);
        }
    }
}
