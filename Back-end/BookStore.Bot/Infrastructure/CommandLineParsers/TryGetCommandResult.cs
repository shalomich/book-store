namespace BookStore.Bot.Infrastructure.CommandLineParsers;
internal record TryGetCommandResult
{
    public string Command { get; private set; }
    public string[] CommandArgs { get; private set; }
    public bool IsComamnd => Command != null;
    public bool HasCommandArgs => CommandArgs != null && CommandArgs.Length > 0;

    public static TryGetCommandResult NotCommand()
    {
        return new TryGetCommandResult();
    }
    public static TryGetCommandResult IsCommand(string command, string[] commandArgs = null)
    {
        if (string.IsNullOrEmpty(command))
        {
            throw new ArgumentException(nameof(command));
        }

        return new TryGetCommandResult
        {
            Command = command,
            CommandArgs = commandArgs
        };
    }
}

