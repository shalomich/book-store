using AutoMapper;
using AutoMapper.QueryableExtensions;
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

namespace BookStore.Application.Queries.Battle.GetBattleInfo;

public record GetBattleInfoQuery() : IRequest<BattleInfoDto>;
internal class GetBattleInfoHandler : IRequestHandler<GetBattleInfoQuery, BattleInfoDto>
{
    private ApplicationContext Context { get; }
    private IMapper Mapper { get; }
    private LoggedUserAccessor LoggedUserAccessor { get; }

    public GetBattleInfoHandler(ApplicationContext context, IMapper mapper, LoggedUserAccessor loggedUserAccessor)
    {
        Context = context;
        Mapper = mapper;
        LoggedUserAccessor = loggedUserAccessor;
    }

    public async Task<BattleInfoDto> Handle(GetBattleInfoQuery request, CancellationToken cancellationToken)
    {
        var currentBattle = Context.Battles
            .Where(battle => battle.IsActive);

        var battleInfo = await currentBattle
            .ProjectTo<BattleInfoDto>(Mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(cancellationToken);

        if (battleInfo == null)
        {
            throw new NotFoundException("There is not active battle.");
        }

        int currentUserId = LoggedUserAccessor.GetCurrentUserId();

        var currentUserVote = await currentBattle
            .SelectMany(battle => battle.BattleBooks)
            .SelectMany(battle => battle.Votes)
            .SingleOrDefaultAsync(vote => vote.UserId == currentUserId, cancellationToken);

        battleInfo = SetVotedStatus(battleInfo, currentUserVote);

        return battleInfo;
    }

    private BattleInfoDto SetVotedStatus(BattleInfoDto battleInfo, Vote currentUserVote)
    {
        if (currentUserVote != null)
        {
            if (currentUserVote.BattleBookId == battleInfo.FirstBattleBook.BattleBookId)
            {
                return battleInfo with
                {
                    FirstBattleBook = battleInfo.FirstBattleBook with
                    {
                        VotedByCurrentUser = true
                    }
                };
            }
            else if (currentUserVote.BattleBookId == battleInfo.SecondBattleBook.BattleBookId)
            {
                return battleInfo with
                {
                    SecondBattleBook = battleInfo.SecondBattleBook with
                    {
                        VotedByCurrentUser = true
                    }
                };
            }
        }

        return battleInfo;
    }
}

