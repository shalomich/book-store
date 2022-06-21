namespace BookStore.Bot.Infrastructure.CommandLineParsers;
internal static class CommandLineParser
{
    public static bool IsCommand(string str)
    {
        return str?.StartsWith('/') ?? false;
    }

    public static TryGetCommandResult FromCommandLine(string str)
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

    public static string ToCommandLine(string commandName, params object[] commandArgs)
    {
        if (commandArgs.Length == 0)
        {
            return $"/{commandName}";
        }
        else
        {
            var commandArgsString = string.Join(' ', commandArgs);

            return $"/{commandName} {commandArgsString}";
        }
    }
}

