using BookStore.Application.Services;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Notifications.BattleStarted;
internal class AccrueVotingPointsForBattleBegunHandler : INotificationHandler<BattleStartedNotification>
{
    private ApplicationContext Context { get; }
    private BattleSettingsProvider BattleSettingsProvider { get; }

    public AccrueVotingPointsForBattleBegunHandler(ApplicationContext context, BattleSettingsProvider battleSettingsProvider)
    {
        Context = context;
        BattleSettingsProvider = battleSettingsProvider;
    }

    public async Task Handle(BattleStartedNotification notification, CancellationToken cancellationToken)
    {
        const int pageSize = 1;
        
        var battleSettings = BattleSettingsProvider.GetBattleSettings();

        var users = Context.Users
            .OrderBy(user => user.Id);

        int pageNumber = 1;

        while (true)
        {
            var userPage = await users
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            if (!userPage.Any())
            {
                break;
            }

            foreach (var user in userPage)
            {
                user.VotingPointCount += battleSettings.VotingPointCountBattleBeginning;
            }

            await Context.SaveChangesAsync(cancellationToken);
            Context.ChangeTracker.Clear();

            pageNumber++;
        }
    }
}

