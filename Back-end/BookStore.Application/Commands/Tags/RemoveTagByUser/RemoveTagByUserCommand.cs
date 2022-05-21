using BookStore.Application.Commands.Tags.Common;
using BookStore.Application.Exceptions;
using BookStore.Application.Services;
using BookStore.Domain.Entities.Products;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Tags.RemoveTagByUser;

public record RemoveTagByUserCommand(UserTagDto TagDto) : IRequest;
internal class RemoveTagByUserCommandHandler : AsyncRequestHandler<RemoveTagByUserCommand>
{
    public ApplicationContext Context { get; }
    public LoggedUserAccessor LoggedUserAccessor { get; }

    public RemoveTagByUserCommandHandler(
        ApplicationContext context,
        LoggedUserAccessor loggedUserAccessor)
    {
        Context = context;
        LoggedUserAccessor = loggedUserAccessor;
    }
    protected override async Task Handle(RemoveTagByUserCommand request, CancellationToken cancellationToken)
    {
        var currentUser = await Context.Users
            .Include(user => user.Tags)
            .SingleAsync(user => user.Id == LoggedUserAccessor.GetCurrentUserId(), cancellationToken);

        var tagById = currentUser.Tags
            .SingleOrDefault(tag => tag.Id == request.TagDto.TagId);

        if (tagById == null)
        {
            throw new NotFoundException(nameof(Tag));
        }

        currentUser.Tags.Remove(tagById);

        await Context.SaveChangesAsync(cancellationToken);
    }
}

