using BookStore.Application.Commands.Account.Login;
using BookStore.TelegramBot.Extensions;
using Telegram.Bot.Types;

namespace BookStore.TelegramBot.UseCases.Authenticate;
internal class AuthenticateMetadataProvider
{
    private Update Update { get; }

    public AuthenticateMetadataProvider(
        Update update)
    {
        Update = update;
    }

    public LoginDto GetLoginData()
    {
        var tryGetCommandResult = Update.TryGetCommand();
        
        if (!tryGetCommandResult.HasCommandArgs)
        {
            throw new ArgumentException("Введите email и пароль от своего аккаунта");
        }

        var commandArgs = tryGetCommandResult.CommandArgs;

        if (commandArgs.Length == 1)
        {
            throw new ArgumentException("Введите пароль от своего аккаунта");
        }

        return new LoginDto
        {
            Email = commandArgs[0],
            Password = commandArgs[1]
        };
    }
}

