using BookStore.Application.Commands.Account.Common;
using BookStore.Application.Commands.Account.Login;
using BookStore.TelegramBot.Domain;
using BookStore.TelegramBot.Providers;
using BookStore.TelegramBot.UseCases.Common;
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
    private BackEndSettings Settings { get; }

    public AuthenticateCommandHandler(
        TelegramBotDbContext dbContext,
        ITelegramBotClient botClient,
        RestClient restClient,
        IOptions<BackEndSettings> settingsOption,
        ILogger<AuthenticateCommandHandler> logger)
    {
        DbContext = dbContext;
        BotClient = botClient;
        RestClient = restClient;
        Logger = logger;
        Settings = settingsOption.Value;
    }

    protected override async Task Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        var update = request.Update;
        var chatId = update.Message.Chat.Id;

        var provider = new AuthenticateCommandProvider(update);

        LoginDto loginData;

        try
        {
            loginData = provider.GetLoginData();
        }
        catch (ArgumentException exception)
        {
            await BotClient.SendTextMessageAsync(
                chatId: chatId,
                text: exception.Message
            );

            return;
        }

        TokensDto tokens = null;

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

            return;
        }

        var user = new TelegramBotUser
        {
            TelegramId = chatId,
            AccessToken = tokens.AccessToken,
            RefreshToken = tokens.RefreshToken,
            AccessTokenExpiration = DateTimeOffset.UtcNow
                .AddMinutes(Settings.AccessTokenExpiredMinutes),
            RefreshTokenExpiration = DateTimeOffset.UtcNow
                .AddMinutes(Settings.RefreshTokenExpiredMinutes)
        };

        await DbContext.TelegramBotUsers.AddAsync(user);
        await DbContext.SaveChangesAsync(cancellationToken);

        await BotClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Авторизация прошла успешна."
        );
    }
}

