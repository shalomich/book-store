
using BookStore.Application.Commands.Tags.Common;
using BookStore.Application.Exceptions;
using BookStore.Application.Services;
using BookStore.Domain.Entities.Products;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Tags.AddTagByUser;

public record AddTagByUserCommand(UserTagDto TagDto) : IRequest;
internal class AddTagByUserCommandHandler : AsyncRequestHandler<AddTagByUserCommand>
{
    public ApplicationContext Context { get; }
    public LoggedUserAccessor LoggedUserAccessor { get; }

    public AddTagByUserCommandHandler(
        ApplicationContext context,
        LoggedUserAccessor loggedUserAccessor)
    {
        Context = context;
        LoggedUserAccessor = loggedUserAccessor;
    }
    protected override async Task Handle(AddTagByUserCommand request, CancellationToken cancellationToken)
    {
        var tagBydId = await Context.Tags
            .SingleOrDefaultAsync(tag => tag.Id == request.TagDto.TagId, cancellationToken);

        if (tagBydId == null)
        {
            throw new NotFoundException(nameof(Tag));
        }

        var currentUser = await Context.Users
            .Include(user => user.Tags)
            .SingleAsync(user => user.Id == LoggedUserAccessor.GetCurrentUserId(), cancellationToken);

        currentUser.Tags.Add(tagBydId);

        await Context.SaveChangesAsync(cancellationToken);
    }
}

