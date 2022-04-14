
using BookStore.Application.Services;
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
    private BattleSettingsProvider BattleSettingsProvider { get; }

    public GetBattlesSettingsHandler(BattleSettingsProvider battleSettingsProvider)
    {
        BattleSettingsProvider = battleSettingsProvider;
    }
    public Task<BattleSettings> Handle(GetBattlesSettingsQuery request, CancellationToken cancellationToken)
    {
        var battleSettings = BattleSettingsProvider.GetBattleSettings();
        
        return Task.FromResult(battleSettings);
    }
}

