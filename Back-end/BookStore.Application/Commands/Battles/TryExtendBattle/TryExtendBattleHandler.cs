using BookStore.Application.Services;
using BookStore.Domain.Entities.Battles;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        var votingPointCounts = await Context.Set<BattleBook>()
            .Where(battleBook => battleBook.Battle.IsActive)
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

        var battle = await Context.Battles
            .SingleAsync(battle => battle.IsActive, cancellationToken);

        var newEndingDate = battle.EndDate.AddHours(extensionHours);

        battle.EndDate = newEndingDate;

        await Context.SaveChangesAsync(cancellationToken);

        return new TryExtendBattleResult
        {
            NeedToExtend = true,
            NewEndDate = newEndingDate
        };
    }
}

