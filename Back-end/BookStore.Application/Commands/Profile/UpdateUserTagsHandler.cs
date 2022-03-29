using BookStore.Application.Exceptions;
using BookStore.Domain.Entities;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Profile;

public record UpdateUserTagsCommand(int UserId, int[] TagIds) : IRequest;
internal class UpdateUserTagsHandler : AsyncRequestHandler<UpdateUserTagsCommand>
{
    private ApplicationContext Context { get; }

    public UpdateUserTagsHandler(ApplicationContext context)
    {
        Context = context;
    }

    protected override async Task Handle(UpdateUserTagsCommand request, CancellationToken cancellationToken)
    {
        var (userId, tagIds) = request;

        var user = await Context.Users
            .Include(user => user.Tags)
            .SingleOrDefaultAsync(user => user.Id == userId, cancellationToken);

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
