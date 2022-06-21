using BookStore.Bot.Extensions;
using BookStore.Bot.Providers;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookStore.Bot.UseCases.Battle.CastVote;
internal class CastVoteCommandProvider
{
    private Update Update { get; }

    public CastVoteCommandProvider(
        Update update)
    {
        Update = update;
    }

    public int GetBattleBookId()
    {
        var tryGetCommandResult = Update.TryGetCommand();

        return int.Parse(tryGetCommandResult.CommandArgs[0]);
    }

    public long GetChatId()
    {
        return Update.CallbackQuery.Message.Chat.Id;
    }

    public InlineKeyboardMarkup GetVotingPointChoiceButtons(int votingPointCount)
    {
        var navigationButtons = new List<InlineKeyboardButton>
        {
            InlineKeyboardButton.WithCallbackData(text: $"Потратить все ({votingPointCount})", callbackData: $"/{CommandNames.SpendVotingPoints} {votingPointCount}"),
            InlineKeyboardButton.WithCallbackData(text: "Выбрать нужное количество", callbackData: $"/{CommandNames.SpendVotingPoints}")
        };

        return new InlineKeyboardMarkup(navigationButtons);
    }
}

