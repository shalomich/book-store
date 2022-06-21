namespace BookStore.TelegramBot.Providers;
internal record BackEndSettings
{
    public string SelectionPath { get; init; }
    public string LoginPath { get; init; }
    public string RefreshTokenPath { get; init; }
    public string UserProfilePath { get; init; }
    public string BattleInfoPath { get; init; }
    public string CastVotePath { get; init; }
    public string SpendVotingPointsPath { get; init; }
    public string BasketPath { get; init; }
    public int AccessTokenExpiredMinutes { get; init; }
    public int RefreshTokenExpiredMinutes { get; init; }
    public int MaxQuantityForChoosenProduct { get; init; }
}

