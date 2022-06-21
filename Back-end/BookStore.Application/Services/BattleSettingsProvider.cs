using BookStore.Application.Queries.Battle.GetBattleSettings;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;

namespace BookStore.Application.Services;
public class BattleSettingsProvider
{
    private readonly string _battleSettingsFilePath;
    public BattleSettingsProvider(IConfiguration configuration)
    {
        string contentRootPath = configuration["ContentRootPath"];

        _battleSettingsFilePath = Path.Combine(contentRootPath, "battlesettings.json");
    }
    public BattleSettings GetBattleSettings()
    {
        var battleSettingsString = File.ReadAllText(_battleSettingsFilePath);

        return JsonConvert.DeserializeObject<BattleSettings>(battleSettingsString);
    }

    public void UpdateBattleSettings(BattleSettings battleSettings)
    {
        using var streamWriter = File.CreateText(_battleSettingsFilePath); ;

        var battleSettingsString = JsonConvert.SerializeObject(battleSettings, Formatting.Indented);

        streamWriter.Write(battleSettingsString);
    }
}

