
using BookStore.Application.Services;
using BookStore.Persistance;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Notifications.BattleBegun;
internal class AccrueVotingPointsForBattleBegunHandler : INotificationHandler<BattleBegunNotification>
{
    private ApplicationContext Context { get; }
    private BattleSettingsProvider BattleSettingsProvider { get; }

    public AccrueVotingPointsForBattleBegunHandler(ApplicationContext context, BattleSettingsProvider battleSettingsProvider)
    {
        Context = context;
        BattleSettingsProvider = battleSettingsProvider;
    }

    public async Task Handle(BattleBegunNotification notification, CancellationToken cancellationToken)
    {
        var battleSettings = BattleSettingsProvider.GetBattleSettings();

        foreach (var user in Context.Users)
        {
            user.VotingPointCount += battleSettings.VotingPointCountBattleBeginning;
        }

        await Context.SaveChangesAsync(cancellationToken);
    }
}

