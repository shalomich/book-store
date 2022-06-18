﻿namespace BookStore.TelegramBot.Providers;
internal record BackEndSettings
{
    public string SelectionPath { get; init; }
    public string LoginPath { get; init; }
    public string RefreshTokenPath { get; init; }
    public string BattleInfoPath { get; init; }
    public int AccessTokenExpiredMinutes { get; init; }
    public int RefreshTokenExpiredMinutes { get; init; }
}
