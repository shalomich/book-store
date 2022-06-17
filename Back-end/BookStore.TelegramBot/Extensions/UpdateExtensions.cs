using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BookStore.TelegramBot.Extensions;
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
        var commandLine = GetCommandLine(update);

        if (commandLine == null)
        {
            return false;
        }

        return commandLine.StartsWith('/');
    }

    public static TryGetCommandResult TryGetCommand(this Update update)
    {
        if (!IsCommand(update))
        { 
            return TryGetCommandResult.NotCommand();
        }

        var commandLine = GetCommandLine(update);

        var commandEndIndex = commandLine.IndexOf(" ");

        bool hasCommandArgs = commandEndIndex != -1;

        string command;

        if (hasCommandArgs)
        {
            command = commandLine.Substring(1, commandEndIndex - 1);
            var commandArgs = commandLine.Substring(commandEndIndex + 1)
                .Split(' ');

            return TryGetCommandResult.IsCommand(command, commandArgs);
        }
        else
        {
            command = commandLine.Substring(1);

            return TryGetCommandResult.IsCommand(command);
        }
    }

    private static string GetCommandLine(Update update)
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
