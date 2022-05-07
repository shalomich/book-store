using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Application.Exceptions;
using BookStore.Application.Services;
using BookStore.Domain.Entities.Battles;
using BookStore.Domain.Enums;
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
    private BattleSettingsProvider BattleSettingsProvider { get; }
    private S3Storage S3Storage { get; }

    public GetBattleInfoHandler(ApplicationContext context, IMapper mapper, LoggedUserAccessor loggedUserAccessor,
        BattleSettingsProvider battleSettingsProvider, S3Storage s3Storage)
    {
        Context = context;
        Mapper = mapper;
        LoggedUserAccessor = loggedUserAccessor;
        BattleSettingsProvider = battleSettingsProvider;
        S3Storage = s3Storage;
    }

    public async Task<BattleInfoDto> Handle(GetBattleInfoQuery request, CancellationToken cancellationToken)
    {
        var battleInfo = await GetCurrentBattle(cancellationToken);

        battleInfo = CalucalateDiscountPercentage(battleInfo);

        battleInfo = SetFileUrls(battleInfo);

        if (LoggedUserAccessor.IsAuthenticated())
        {
            var currentUserId = LoggedUserAccessor.GetCurrentUserId();

            battleInfo = await SetCurrentUserBattleInfo(battleInfo, currentUserId, cancellationToken);
        }

        var battleSettings = BattleSettingsProvider.GetBattleSettings();

        return battleInfo with
        {
            InitialDiscount = battleSettings.InitialDiscount,
            FinalDiscount = battleSettings.FinalDiscount
        };
    }

    private async Task<BattleInfoDto> GetCurrentBattle(CancellationToken cancellationToken)
    {
        var currentBattle = Context.Battles
           .Where(battle => battle.State != BattleState.Finished);

        var battleInfo = await currentBattle
            .ProjectTo<BattleInfoDto>(Mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(cancellationToken);

        if (battleInfo == null)
        {
            var prevoiusBattles = Context.Battles
                .Where(battle => battle.State == BattleState.Finished)
                .OrderByDescending(battle => battle.EndDate);

            battleInfo = await prevoiusBattles
                .ProjectTo<BattleInfoDto>(Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (battleInfo == null)
            {
                throw new NotFoundException("There is no battle.");
            }
        }

        return battleInfo;
    }

    private BattleInfoDto CalucalateDiscountPercentage(BattleInfoDto battleInfo)
    {
        var battleSettings = BattleSettingsProvider.GetBattleSettings();

        int totalBattleVotingPointCount = battleInfo.FirstBattleBook.TotalVotingPointCount + battleInfo.SecondBattleBook.TotalVotingPointCount;

        int currentDiscount = BattleCalculator.CalculateDiscount(totalBattleVotingPointCount, battleSettings);

        return battleInfo with
        {
            DiscountPercentage = currentDiscount
        };
    }

    private BattleInfoDto SetFileUrls(BattleInfoDto battleInfo)
    {
        var firstTitleImage = battleInfo.FirstBattleBook.TitleImage with
        {
            FileUrl = S3Storage.GetPresignedUrlForViewing(battleInfo.FirstBattleBook.BookId, battleInfo.FirstBattleBook.TitleImage.Id)
        };

        var secondTitleImage = battleInfo.SecondBattleBook.TitleImage with
        {
            FileUrl = S3Storage.GetPresignedUrlForViewing(battleInfo.SecondBattleBook.BookId, battleInfo.SecondBattleBook.TitleImage.Id)
        };

        return battleInfo with
        {
            FirstBattleBook = battleInfo.FirstBattleBook with { TitleImage = firstTitleImage },
            SecondBattleBook = battleInfo.SecondBattleBook with { TitleImage = secondTitleImage }
        };
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

