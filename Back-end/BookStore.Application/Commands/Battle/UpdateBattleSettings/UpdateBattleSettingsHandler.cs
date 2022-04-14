using BookStore.Application.Queries.Battle.GetBattleSettings;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Battle.UpdateBattleSettings;

public record UpdateBattleSettingsCommand(BattleSettings BattleSettings) : IRequest;
internal class UpdateBattleSettingsHandler : AsyncRequestHandler<UpdateBattleSettingsCommand>
{
    private string ContentRootPath { get; }

    public UpdateBattleSettingsHandler(IConfiguration configuration)
    {
        ContentRootPath = configuration[nameof(ContentRootPath)];
    }

    protected override Task Handle(UpdateBattleSettingsCommand request, CancellationToken cancellationToken)
    {
        var battleSettingsFilePath = Path.Combine(ContentRootPath, "battlesettings.json");

        using var streamWriter = File.CreateText(battleSettingsFilePath); ;

        var battleSettingsString = JsonConvert.SerializeObject(request.BattleSettings, Formatting.Indented);

        streamWriter.Write(battleSettingsString);

        return Task.CompletedTask;
    }
}

