using BookStore.Application.Commands.Account.Common;
using BookStore.Application.Commands.Account.Login;
using BookStore.TelegramBot.Domain;
using BookStore.TelegramBot.Providers;
using BookStore.TelegramBot.UseCases.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BookStore.TelegramBot.UseCases.Authenticate;

internal record AuthenticateCommand(Update Update) : TelegramBotCommand(Update);
internal class AuthenticateCommandHandler : TelegramBotCommandHandler<AuthenticateCommand>
{
    private TelegramBotDbContext DbContext { get; }
    private ITelegramBotClient BotClient { get; }
    private RestClient RestClient { get; }
    private ILogger<AuthenticateCommandHandler> Logger { get; }
    private CallbackCommandRepository CallbackCommandRepository { get; }
    private BackEndSettings Settings { get; }

    public AuthenticateCommandHandler(
        TelegramBotDbContext dbContext,
        ITelegramBotClient botClient,
        RestClient restClient,
        IOptions<BackEndSettings> settingsOption,
        ILogger<AuthenticateCommandHandler> logger,
        CallbackCommandRepository callbackCommandRepository)
    {
        DbContext = dbContext;
        BotClient = botClient;
        RestClient = restClient;
        Logger = logger;
        CallbackCommandRepository = callbackCommandRepository;
        Settings = settingsOption.Value;
    }

    protected override async Task Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        var provider = new AuthenticateCommandProvider(request.Update);

        var chatId = provider.GetChatId();
        bool isCallback = await CallbackCommandRepository.HasAsync(CommandNames.Authenticate, telegramId: chatId,
            cancellationToken);
        
        provider.IsCallback = isCallback;

        var (loginData, successToGetLoginData) = await TryGetLoginDataAsync(provider, cancellationToken);

        if (!successToGetLoginData)
        {
            return;
        }

        var (tokens, successToAuthenticate) = await TryAuthenticateAsync(loginData, provider, cancellationToken);
        
        if (!successToAuthenticate)
        {
            return;
        }

        await UpdateTokensAsync(tokens, provider, cancellationToken);
    }

    private async Task<(LoginDto LoginData, bool Success)> TryGetLoginDataAsync(AuthenticateCommandProvider provider,
        CancellationToken cancellationToken)
    {
        LoginDto loginData = null;
        var chatId = provider.GetChatId();

        if (provider.IsCallback)
        {
            var commandLine = await CallbackCommandRepository.GetAsync(telegramId: chatId, cancellationToken);
            loginData = provider.GetLoginData(commandLine);
        }
        else
        {
            loginData = provider.GetLoginData();
        }

        bool hasEmail = loginData.Email != null;
        bool hasPassword = loginData.Password!= null;

        if (hasEmail && hasPassword)
        {
            return (loginData, true);
        }

        string message = null;
        string callbackCommandLine = null;

        if (!hasEmail)
        {
            message = "Введите email.";
            callbackCommandLine = CommandLineParser.ToCommandLine(CommandNames.Authenticate);
        }

        if (hasEmail && !hasPassword)
        {
            message = "Введите пароль.";
            callbackCommandLine = CommandLineParser.ToCommandLine(CommandNames.Authenticate, loginData.Email);
        }

        await BotClient.SendTextMessageAsync(chatId, message, cancellationToken: cancellationToken);

        await CallbackCommandRepository.UpdateAsync(callbackCommandLine, telegramId: chatId, cancellationToken);

        return (loginData, false);
    }

    private async Task<(TokensDto Tokens, bool Success)> TryAuthenticateAsync(LoginDto loginData,
        AuthenticateCommandProvider provider, CancellationToken cancellationToken)
    {
        TokensDto tokens = null;
        var chatId = provider.GetChatId();

        try
        {
            tokens = await RestClient.PostAsync<TokensDto>(new RestRequest(Settings.LoginPath)
                .AddBody(loginData));
        }
        catch (Exception exception)
        {
            Logger.LogError(exception, "Fail to authenticate telegram bot with store.");

            await BotClient.SendTextMessageAsync(
               chatId: chatId,
               text: "Авторизация провалена. Проверьте свой логин и пароль."
            );

            await CallbackCommandRepository.RemoveAsync(CommandNames.Authenticate, telegramId: chatId, cancellationToken);
            
            return (tokens, false);
        }

        return (tokens, true);
    }

    private async Task UpdateTokensAsync(TokensDto tokens, AuthenticateCommandProvider provider, 
        CancellationToken cancellationToken)
    {
        var chatId = provider.GetChatId();

        var user = await DbContext.TelegramBotUsers
            .SingleOrDefaultAsync(user => user.TelegramId == chatId, cancellationToken);

        if (user == null)
        {
            user = new TelegramBotUser()
            {
                TelegramId = chatId
            };

            DbContext.TelegramBotUsers.Add(user);
        }

        user.SetUserInfo(tokens, Settings);

        await DbContext.SaveChangesAsync(cancellationToken);

        if (provider.IsCallback)
        {
            await CallbackCommandRepository.RemoveAsync(CommandNames.Authenticate, telegramId: chatId, cancellationToken);
        }

        await BotClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Авторизация прошла успешна."
        );
    }
}

