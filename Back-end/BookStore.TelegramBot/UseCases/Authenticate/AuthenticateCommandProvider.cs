using BookStore.Application.Commands.Account.Login;
using BookStore.TelegramBot.Extensions;
using BookStore.TelegramBot.UseCases.Common;
using Telegram.Bot.Types;

namespace BookStore.TelegramBot.UseCases.Authenticate;
internal class AuthenticateCommandProvider
{
    private Update Update { get; }
    public bool IsCallback { get; set; }

    public AuthenticateCommandProvider(
        Update update)
    {
        Update = update;
    }

    public long GetChatId()
    {
        return Update.Message.Chat.Id;
    }

    public LoginDto GetLoginData(string commandLine = null)
    {
        TryGetCommandResult tryGetCommandResult;
        
        if (commandLine == null)
        {
            tryGetCommandResult = Update.TryGetCommand();
        }
        else
        {
            var commandArg = Update.Message.Text.Split(' ')[0];
            commandLine += " " + commandArg;
            tryGetCommandResult = CommandLineParser.FromCommandLine(commandLine);
        }
        
        if (!tryGetCommandResult.HasCommandArgs)
        {
            return new LoginDto();
        }

        var commandArgs = tryGetCommandResult.CommandArgs;

        if (commandArgs.Length == 1)
        {
            return new LoginDto
            {
                Email = commandArgs[0]
            };
        }

        return new LoginDto
        {
            Email = commandArgs[0],
            Password = commandArgs[1]
        };
    }
}

