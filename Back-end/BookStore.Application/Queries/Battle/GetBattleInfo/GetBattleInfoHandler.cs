﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Application.Commands.BookEditing.Common;
using BookStore.Application.Exceptions;
using BookStore.Application.Services;
using BookStore.Domain.Enums;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Queries.Battle.GetBattleInfo;

public record GetBattleInfoQuery() : IRequest<BattleInfoDto>;
internal class GetBattleInfoHandler : IRequestHandler<GetBattleInfoQuery, BattleInfoDto>
{
    private ApplicationContext Context { get; }
    private IMapper Mapper { get; }
    private BattleSettingsProvider BattleSettingsProvider { get; }
    private ImageFileRepository ImageFileRepository { get; }

    public GetBattleInfoHandler(
        ApplicationContext context, 
        IMapper mapper, 
        BattleSettingsProvider battleSettingsProvider, 
        ImageFileRepository imageFileRepository)
    {
        Context = context;
        Mapper = mapper;
        BattleSettingsProvider = battleSettingsProvider;
        ImageFileRepository = imageFileRepository;
    }

    public async Task<BattleInfoDto> Handle(GetBattleInfoQuery request, CancellationToken cancellationToken)
    {
        var battleInfo = await GetCurrentBattle(cancellationToken);

        battleInfo = CalucalateDiscountPercentage(battleInfo);

        battleInfo = await SetFileUrls(battleInfo, cancellationToken);

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

    private async Task<BattleInfoDto> SetFileUrls(BattleInfoDto battleInfo, CancellationToken cancellationToken)
    {
        var firstTitleImage = battleInfo.FirstBattleBook.TitleImage with
        {
            FileUrl = await ImageFileRepository.GetPresignedUrlForViewing(battleInfo.FirstBattleBook.TitleImage.Id, cancellationToken)
        };

        var secondTitleImage = battleInfo.SecondBattleBook.TitleImage with
        {
            FileUrl = await ImageFileRepository.GetPresignedUrlForViewing(battleInfo.SecondBattleBook.TitleImage.Id, cancellationToken)
        };

        return battleInfo with
        {
            FirstBattleBook = battleInfo.FirstBattleBook with { TitleImage = firstTitleImage },
            SecondBattleBook = battleInfo.SecondBattleBook with { TitleImage = secondTitleImage }
        };
    }
}

