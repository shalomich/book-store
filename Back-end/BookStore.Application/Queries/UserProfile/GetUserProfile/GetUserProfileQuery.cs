using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Application.Services;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Queries.UserProfile.GetUserProfile;
public record GetUserProfileQuery() : IRequest<UserProfileDto>;
internal class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfileDto>
{
    private ApplicationContext Context { get; }
    private IMapper Mapper { get; }
    public LoggedUserAccessor LoggedUserAccessor { get; }

    public GetUserProfileQueryHandler(
        ApplicationContext context, 
        IMapper mapper,
        LoggedUserAccessor loggedUserAccessor)
    {
        Context = context;
        Mapper = mapper;
        LoggedUserAccessor = loggedUserAccessor;
    }

    public async Task<UserProfileDto> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = LoggedUserAccessor.GetCurrentUserId();

        var currentUser = await Context.Users
            .Include(user => user.Votes
                .Where(vote => vote.BattleBook.Battle.State != Domain.Enums.BattleState.Finished))
            .Include(user => user.Marks)
            .Include(user => user.BasketProducts)
            .Include(user => user.TelegramBotContact)
            .Include(user => user.Tags)
            .SingleAsync(user => user.Id == currentUserId);

        return Mapper.Map<UserProfileDto>(currentUser);
    }
}
