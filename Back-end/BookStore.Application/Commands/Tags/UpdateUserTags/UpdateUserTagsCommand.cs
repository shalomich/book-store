using BookStore.Application.Exceptions;
using BookStore.Application.Services;
using BookStore.Domain.Entities.Products;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Tags.UpdateUserTags
{
    public record UpdateUserTagsCommand(UserTagsDto TagDto) : IRequest;
    internal class UpdateUserTagsCommandHandler : AsyncRequestHandler<UpdateUserTagsCommand>
    {
        public ApplicationContext Context { get; }
        public LoggedUserAccessor LoggedUserAccessor { get; }

        public UpdateUserTagsCommandHandler(
            ApplicationContext context,
            LoggedUserAccessor loggedUserAccessor)
        {
            Context = context;
            LoggedUserAccessor = loggedUserAccessor;
        }
        protected override async Task Handle(UpdateUserTagsCommand request, CancellationToken cancellationToken)
        {
            var tagIds = request.TagDto.TagIds;

            var newTags = await Context.Tags
                .Where(tag => tagIds.Contains(tag.Id))
                .ToListAsync(cancellationToken);

            var notExistTagIds = tagIds.Except(newTags
                .Select(tag => tag.Id));

            if (notExistTagIds.Any())
            {
                var notExistTagIdsString = notExistTagIds
                    .Select(id => id.ToString())
                    .Aggregate((id1, id2) => $"{id1}, {id2}");

                throw new NotFoundException("There are not tags with ids: " + notExistTagIdsString);
            }

            var currentUser = await Context.Users
                .Include(user => user.Tags)
                .SingleAsync(user => user.Id == LoggedUserAccessor.GetCurrentUserId(), cancellationToken);

            currentUser.Tags = newTags.ToHashSet();

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}