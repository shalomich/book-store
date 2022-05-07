using BookStore.Application.Exceptions;
using BookStore.Application.Services;
using BookStore.Domain.Enums;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Battles;

public record SpendVotingPointsCommand(int VotingPointCount) : IRequest;
internal class SpendVotingPointsHandler : AsyncRequestHandler<SpendVotingPointsCommand>
{
    private ApplicationContext Context { get; }
    private LoggedUserAccessor LoggedUserAccessor { get; }

    public SpendVotingPointsHandler(ApplicationContext context, LoggedUserAccessor loggedUserAccessor)
    {
        Context = context;
        LoggedUserAccessor = loggedUserAccessor;
    }

    protected override async Task Handle(SpendVotingPointsCommand request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);

        int currentUserId = LoggedUserAccessor.GetCurrentUserId();

        var currentUser = await Context.Users
            .SingleAsync(user => user.Id == currentUserId, cancellationToken);

        var currentUserVote = await Context.Votes
            .Where(vote => vote.BattleBook.Battle.State != BattleState.Finished)
            .SingleAsync(vote => vote.UserId == currentUserId, cancellationToken);

        currentUserVote.VotingPointCount += request.VotingPointCount;
        currentUser.VotingPointCount -= request.VotingPointCount;

        await Context.SaveChangesAsync(cancellationToken);
    }

    private async Task Validate(SpendVotingPointsCommand request, CancellationToken cancellationToken)
    {
        int currentUserId = LoggedUserAccessor.GetCurrentUserId();

        var existCurrentUserVote = await Context.Battles
            .Where(battle => battle.State != BattleState.Finished)
            .SelectMany(battle => battle.BattleBooks)
            .SelectMany(battleBook => battleBook.Votes)
            .AnyAsync(vote => vote.UserId == currentUserId, cancellationToken);

        if (!existCurrentUserVote)
        {
            throw new NotFoundException("Current user has no vote in current battle.");
        }

        int userVotingPointCount = await Context.Users
            .Where(user => user.Id == currentUserId)
            .Select(user => user.VotingPointCount)
            .SingleAsync(cancellationToken);

        if (request.VotingPointCount > userVotingPointCount)
        {
            throw new BadRequestException("Not enough voting points.");
        }
    }
}

