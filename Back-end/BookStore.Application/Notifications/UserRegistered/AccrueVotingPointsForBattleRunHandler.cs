using BookStore.Application.Services;
using BookStore.Domain.Enums;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Notifications.UserRegistered;
internal class AccrueVotingPointsForBattleRunHandler : INotificationHandler<UserRegisteredNotification>
{
    private ApplicationContext Context { get; }
    private BattleSettingsProvider BattleSettingsProvider { get; }

    public AccrueVotingPointsForBattleRunHandler(
        ApplicationContext context, 
        BattleSettingsProvider battleSettingsProvider)
    {
        Context = context;
        BattleSettingsProvider = battleSettingsProvider;
    }

    public async Task Handle(UserRegisteredNotification notification, CancellationToken cancellationToken)
    {
        bool battleIsRun = await Context.Battles
            .AnyAsync(battle => battle.State != BattleState.Finished);

        if (!battleIsRun)
        {
            return;
        }

        var userById = await Context.Users
            .SingleOrDefaultAsync(user => user.Id == notification.UserId);

        var battleSettings = BattleSettingsProvider.GetBattleSettings();

        userById.VotingPointCount = battleSettings.VotingPointCountBattleBeginning;

        await Context.SaveChangesAsync(cancellationToken);
    }
}

