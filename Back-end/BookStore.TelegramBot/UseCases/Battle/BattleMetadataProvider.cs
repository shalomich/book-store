﻿using BookStore.Domain.Enums;
using System.Text;
using Telegram.Bot.Types;

namespace BookStore.TelegramBot.UseCases.Battle;
internal class BattleMetadataProvider
{
    private Update Update { get; }
    public BattleMetadataProvider(
        Update update)
    {
        Update = update;
    }

    public string GetBattleInfoHtml(BattleInfoViewModel battleInfo)
    {
        var builder = new StringBuilder();

        var lefTime = battleInfo.LeftTime;
        var leftTimeString = $"дней - {lefTime.Days}, часов - {lefTime.Hours}, минут - {lefTime.Minutes}.";
        
        var battleStateMessage = battleInfo.State switch
        {
            BattleState.Started => $"Баттл комиксов закончится через: {leftTimeString}",
            BattleState.Extended => $"Баттл комиксов продлён и закончится через {leftTimeString}",
            BattleState.Finished => "Баттл комиксов закончен"
        };

        builder.Append($"<b>{battleStateMessage}</b>\n");
        builder.Append($"<b>Текущая скидка: </b> {battleInfo.CurrentDiscount}\n");

        if (battleInfo.LeaderBattleBookName != null)
        {
            builder.Append($"<b>Лидер: </b> {battleInfo.LeaderBattleBookName}");
        }
        
        return builder.ToString();
    }

    public string GetBattleBookHtml(BattleBookViewModel battleBook, BattleInfoViewModel battleInfo)
    {
        var builder = new StringBuilder();
        
        var discountCost = battleInfo.CurrentDiscount / 100.0 * battleBook.Cost;

        builder.Append($"<b>Название: </b>" +
            $"<a href=\"{battleBook.StoreUrl}\"><i>{battleBook.Name}</i></a>\n");
        builder.Append($"<b>Автор: </b><i>{battleBook.AuthorName}</i>\n");
        builder.Append($"<b>Цена: </b> {battleBook.Cost}\n");

        builder.Append($"<u><b>Цена со скидкой: </b>{discountCost}</u>\n");
        builder.Append($"<u><b>Количество голосов: </b>{battleBook.TotalVotingPointCount}</u>\n");

        return builder.ToString();
    }
}

