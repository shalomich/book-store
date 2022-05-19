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

namespace BookStore.Application.Commands.Battles.TryExtendBattle;

public record TryExtendBattleCommand() : IRequest<TryExtendBattleResult>;
internal class TryExtendBattleHandler : IRequestHandler<TryExtendBattleCommand, TryExtendBattleResult>
{
    private ApplicationContext Context { get; }
    private BattleSettingsProvider BattleSettingsProvider { get; }

    public TryExtendBattleHandler(ApplicationContext context, BattleSettingsProvider battleSettingsProvider)
    {
        Context = context;
        BattleSettingsProvider = battleSettingsProvider;
    }

    public async Task<TryExtendBattleResult> Handle(TryExtendBattleCommand request, CancellationToken cancellationToken)
    {
        Battle currentBattle;

        try
        {
            currentBattle = await Context.Battles
                .Where(battle => battle.State != BattleState.Finished)
                .SingleOrDefaultAsync(cancellationToken);
        }
        catch (InvalidOperationException)
        {
            throw new InvalidOperationException("There are several not finished battles.");
        }

        if (currentBattle == null)
        {
            throw new BadRequestException("There is no battle with not finished state.");
        }

        var votingPointCounts = await Context.Set<BattleBook>()
            .Where(battleBook => battleBook.Battle.State != BattleState.Finished)
            .Select(battleBook => battleBook.Votes
                .Sum(vote => vote.VotingPointCount))
            .ToListAsync(cancellationToken);

        var firstBookVotingPointCounts = votingPointCounts.First();
        var secondBookVotingPointCounts = votingPointCounts.Last();

        if (firstBookVotingPointCounts != secondBookVotingPointCounts)
        {
            return new TryExtendBattleResult { NeedToExtend = false };
        }

        var extensionHours = BattleSettingsProvider.GetBattleSettings().BattleExtensionInHours;

        var newEndingDate = currentBattle.EndDate.AddHours(extensionHours);

        currentBattle.EndDate = newEndingDate;
        currentBattle.State = BattleState.Extended;

        await Context.SaveChangesAsync(cancellationToken);

        return new TryExtendBattleResult
        {
            NeedToExtend = true,
            NewEndDate = newEndingDate
        };
    }
}

