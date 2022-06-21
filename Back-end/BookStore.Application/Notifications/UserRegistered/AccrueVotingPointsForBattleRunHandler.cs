using BookStore.Application.Services;
using BookStore.Domain.Enums;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Notifications.UserRegistered;
internal class AccrueVotingPointsForBattleRunHandler : INotificationHandler<UserRegisteredNotification>
{
    private ApplicationContext Context { get; }
    private BattleSettingsProvider BattleSettingsProvider { get; }
    private ILogger<AccrueVotingPointsForBattleRunHandler> Logger { get; }

    public AccrueVotingPointsForBattleRunHandler(
        ApplicationContext context, 
        BattleSettingsProvider battleSettingsProvider,
        ILogger<AccrueVotingPointsForBattleRunHandler> logger)
    {
        Context = context;
        BattleSettingsProvider = battleSettingsProvider;
        Logger = logger;
    }

    public async Task Handle(UserRegisteredNotification notification, CancellationToken cancellationToken)
    {
        bool battleIsRun = await Context.Battles
            .AnyAsync(battle => battle.State != BattleState.Finished);

        if (!battleIsRun)
        {
            Logger.LogWarning("User doesn't get voting points because battle doesn't run");

            return;
        }

        var userById = await Context.Users
            .SingleOrDefaultAsync(user => user.Id == notification.UserId);

        var battleSettings = BattleSettingsProvider.GetBattleSettings();

        userById.VotingPointCount = battleSettings.VotingPointCountBattleBeginning;

        await Context.SaveChangesAsync(cancellationToken);

        Logger.LogInformation("User get voting points after registration");
    }
}

