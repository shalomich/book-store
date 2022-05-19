using BookStore.Application.Exceptions;
using BookStore.Application.Services;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.UserProfile.UpdateUserTags;

public record UpdateUserTagsCommand(int[] TagIds) : IRequest;
internal class UpdateUserTagsHandler : AsyncRequestHandler<UpdateUserTagsCommand>
{
    private ApplicationContext Context { get; }
    public LoggedUserAccessor LoggedUserAccessor { get; }

    public UpdateUserTagsHandler(
        ApplicationContext context,
        LoggedUserAccessor loggedUserAccessor)
    {
        Context = context;
        LoggedUserAccessor = loggedUserAccessor;
    }

    protected override async Task Handle(UpdateUserTagsCommand request, CancellationToken cancellationToken)
    {
        var tagIds = request.TagIds;

        var user = await Context.Users
            .Include(user => user.Tags)
            .SingleOrDefaultAsync(user => user.Id == LoggedUserAccessor.GetCurrentUserId(), cancellationToken);

        if (tagIds.Length == 0)
        {
            user.Tags = null;
        }
        else
        {
            var tags = await Context.Tags
                .Where(tag => tagIds.Contains(tag.Id))
                .ToListAsync(cancellationToken);

            if (tags.Count() != tagIds.Distinct().Count())
            {
                throw new BadRequestException("Invalid tag ids.");
            }

            user.Tags = tags.ToHashSet();
        }

        await Context.SaveChangesAsync(cancellationToken);
    }
}
