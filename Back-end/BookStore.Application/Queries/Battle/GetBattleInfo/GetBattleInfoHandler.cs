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
            var prevoiusBattles = Context.Battles
                .Where(battle => !battle.IsActive)
                .OrderByDescending(battle => battle.EndDate);

            battleInfo = await prevoiusBattles
                .ProjectTo<BattleInfoDto>(Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (battleInfo == null)
            {
                throw new NotFoundException("There is not active battle.");
            }
        }

        if (LoggedUserAccessor.IsAuthenticated())
        {
            var currentUserId = LoggedUserAccessor.GetCurrentUserId();

            return await SetCurrentUserBattleInfo(battleInfo, currentUserId, cancellationToken);
        }

        return battleInfo;
    }

    private async Task<BattleInfoDto> SetCurrentUserBattleInfo(BattleInfoDto battleInfo, int currentUserId,
        CancellationToken cancellationToken)
    {
        var currentUserVote = await Context
            .Set<BattleBook>()
            .Where(battleBook => battleBook.BattleId == battleInfo.Id)
            .SelectMany(battle => battle.Votes)
            .SingleOrDefaultAsync(vote => vote.UserId == currentUserId, cancellationToken);

        if (currentUserVote != null)
        {
            return battleInfo with
            {
                VotedBattleBookId = currentUserVote.BattleBookId,
                SpentVotingPointCount = currentUserVote.VotingPointCount
            };
        }

        return battleInfo;
    }
}

