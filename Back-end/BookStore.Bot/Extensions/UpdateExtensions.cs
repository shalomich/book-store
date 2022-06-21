using BookStore.Bot.Infrastructure.CommandLineParsers;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BookStore.Bot.Extensions;
internal static class UpdateExtensions
{
    public static long GetChatId(this Update update)
    {
        if (update.Type == UpdateType.Message)
        {
            return update.Message.Chat.Id;
        }

        if (update.Type == UpdateType.CallbackQuery)
        {
            return update.CallbackQuery.Message.Chat.Id;
        }

        throw new InvalidOperationException();
    }

    public static bool IsCommand(this Update update)
    {  
        return CommandLineParser.IsCommand(GetCommandLine(update));
    }

    public static TryGetCommandResult TryGetCommand(this Update update)
    {
        return CommandLineParser.FromCommandLine(GetCommandLine(update));
    }

    public static string GetCommandLine(Update update)
    {
        if (update.Type == UpdateType.Message)
        {
            return update.Message.Text;
        }

        if (update.Type == UpdateType.CallbackQuery)
        {
            return update.CallbackQuery.Data;
        }

        return null;
    }
}
