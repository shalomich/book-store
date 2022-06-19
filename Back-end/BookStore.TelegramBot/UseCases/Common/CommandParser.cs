namespace BookStore.TelegramBot.UseCases.Common;
internal static class CommandParser
{
    public static bool IsCommand(string str)
    {
        return str?.StartsWith('/') ?? false;
    }

    public static TryGetCommandResult TryGetCommand(string str)
    {
        if (!IsCommand(str))
        {
            return TryGetCommandResult.NotCommand();
        }

        var commandEndIndex = str.IndexOf(" ");

        bool hasCommandArgs = commandEndIndex != -1;

        string command;

        if (hasCommandArgs)
        {
            command = str.Substring(1, commandEndIndex - 1);
            var commandArgs = str.Substring(commandEndIndex + 1)
                .Split(' ');

            return TryGetCommandResult.IsCommand(command, commandArgs);
        }
        else
        {
            command = str.Substring(1);

            return TryGetCommandResult.IsCommand(command);
        }
    }
}

