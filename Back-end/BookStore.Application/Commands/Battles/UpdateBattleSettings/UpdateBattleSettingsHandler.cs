using BookStore.Application.Queries.Battle.GetBattleSettings;
using BookStore.Application.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Battles.UpdateBattleSettings;

public record UpdateBattleSettingsCommand(BattleSettings BattleSettings) : IRequest;
internal class UpdateBattleSettingsHandler : AsyncRequestHandler<UpdateBattleSettingsCommand>
{
    private BattleSettingsProvider BattleSettingsProvider { get; }

    public UpdateBattleSettingsHandler(BattleSettingsProvider battleSettingsProvider)
    {
        BattleSettingsProvider = battleSettingsProvider;
    }

    protected override Task Handle(UpdateBattleSettingsCommand request, CancellationToken cancellationToken)
    {
        BattleSettingsProvider.UpdateBattleSettings(request.BattleSettings);

        return Task.CompletedTask;
    }
}

