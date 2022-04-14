
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Queries.Battle.GetBattleSettings;

public record GetBattlesSettingsQuery() : IRequest<BattleSettings>;
internal class GetBattlesSettingsHandler : IRequestHandler<GetBattlesSettingsQuery, BattleSettings>
{
    private string ContentRootPath { get; }

    public GetBattlesSettingsHandler(IConfiguration configuration)
    {
        ContentRootPath = configuration[nameof(ContentRootPath)];
    }
    public Task<BattleSettings> Handle(GetBattlesSettingsQuery request, CancellationToken cancellationToken)
    {
        var battleSettingsFilePath = Path.Combine(ContentRootPath, "battlesettings.json");

        BattleSettings battleSettings;
        string battleSettingsString;

        if (!File.Exists(battleSettingsFilePath))
        {
            battleSettings = new BattleSettings();

            battleSettingsString = JsonConvert.SerializeObject(battleSettings, Formatting.Indented);

            using var streamWriter = File.CreateText(battleSettingsFilePath);
            
            streamWriter.Write(battleSettingsString);
        }
        else
        {
            battleSettingsString = File.ReadAllText(battleSettingsFilePath);

            battleSettings = JsonConvert.DeserializeObject<BattleSettings>(battleSettingsString);
        }
       
        return Task.FromResult(battleSettings);
    }
}

