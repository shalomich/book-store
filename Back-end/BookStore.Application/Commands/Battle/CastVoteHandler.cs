using BookStore.Application.Exceptions;
using BookStore.Application.Services;
using BookStore.Domain.Entities.Battles;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Battle;

public record CastVoteCommand(int BattleBookId) : IRequest;
internal class CastVoteHandler : AsyncRequestHandler<CastVoteCommand>
{
    private ApplicationContext Context { get; }
    private LoggedUserAccessor LoggedUserAccessor { get; }

    public CastVoteHandler(ApplicationContext context, LoggedUserAccessor loggedUserAccessor)
    {
        Context = context;
        LoggedUserAccessor = loggedUserAccessor;
    }

    protected override async Task Handle(CastVoteCommand request, CancellationToken cancellationToken)
    {
        await Vaidate(request, cancellationToken);

        int currentUserId = LoggedUserAccessor.GetCurrentUserId();

        var vote = new Vote
        {
            BattleBookId = request.BattleBookId,
            UserId = currentUserId,
        };

        Context.Add(vote);

        await Context.SaveChangesAsync(cancellationToken);
    }

    private async Task Vaidate(CastVoteCommand request, CancellationToken cancellationToken)
    {
        var currentBattleBooks = Context.Battles
            .Where(battle => battle.IsActive)
            .SelectMany(battle => battle.BattleBooks);

        bool existBattleBook = await currentBattleBooks
            .AnyAsync(battleBook => battleBook.Id == request.BattleBookId, cancellationToken);

        if (!existBattleBook)
        {
            throw new NotFoundException("There is not battle book with this id in current battle");
        }

        int currentUserId = LoggedUserAccessor.GetCurrentUserId();

        bool existCurrentUserVote = await currentBattleBooks
            .SelectMany(battleBook => battleBook.Votes)
            .AnyAsync(vote => vote.UserId == currentUserId, cancellationToken);

        if (existCurrentUserVote)
        {
            throw new BadRequestException("Current user already had vote in current battle.");
        }
    }
}

