using BookStore.Application.Services.Jwt;
using BookStore.Domain.Entities;
using BookStore.Persistance;
using BookStore.TelegramBot.UseCases.Common;
using BookStore.TelegramBot.UseCases.TryConnectToStore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BookStore.TelegramBot.UseCases.AuthenticateTelegramBotContact;

public record TryConnectToStoreCommand(Message Message, bool IsStartCommand) : IRequest<bool>;
internal class TryConnectToStoreCommandHandler : IRequestHandler<TryConnectToStoreCommand, bool>
{ 
    private ApplicationContext Context { get; }
    private ITelegramBotClient BotClient { get; }
    private TelegramBotJwtParser JwtParser { get; }
    private ILogger Logger { get; }

    public TryConnectToStoreCommandHandler(ApplicationContext context, ITelegramBotClient botClient,
        TelegramBotJwtParser jwtParser, ILogger<TryConnectToStoreCommandHandler> logger)
    {
        Context = context;
        BotClient = botClient;
        JwtParser = jwtParser;
        Logger = logger;
    }

    public async Task<bool> Handle(TryConnectToStoreCommand request, CancellationToken cancellationToken)
    {
        var (message, isStartCommand) = request;

        var registerStatus = await GetRegisterBotContactStatus(message, isStartCommand, cancellationToken);

        switch ((isStartCommand, registerStatus))
        {
            case (isStartCommand: true, registerStatus: RegisterBotContactStatus.Ready):

                await BotClient.SendTextMessageAsync(message.Chat.Id, "Бот уже связан с онлайн магазином.");
                break;

            case (isStartCommand: true, registerStatus: RegisterBotContactStatus.Invalid):

                await BotClient.SendTextMessageAsync(message.Chat.Id, "Необходимо залогиниться через сайт по ссылке: ");
                return false;
            
            case (isStartCommand: true, registerStatus: RegisterBotContactStatus.Success):

                await BotClient.SendTextMessageAsync(message.Chat.Id, "Бот теперь связан с вашем аккаунтом на нашем сайте. Для аутентификации вашего телеграм аккаунта предоставьте номер телефона.");
                return false;
            
            case (isStartCommand: false, registerStatus: RegisterBotContactStatus.Invalid):

                await BotClient.SendTextMessageAsync(message.Chat.Id, "Необходимо авторизоваться через сайт по ссылке: ");
                return false;
        }

        var authenticateStatus = await GetAuthenticateTelgramUserStatus(message, cancellationToken);

        switch (authenticateStatus)
        {
            case AuthenticateTelegramUserStatus.Ready:
                break;

            case AuthenticateTelegramUserStatus.HasNoPhone:

                await BotClient.SendTextMessageAsync(message.Chat.Id, "Для подтверждения аккаунта предоставьте номер телефона.");
                return false;
            
            case AuthenticateTelegramUserStatus.DifferentPhones:
            
                await BotClient.SendTextMessageAsync(message.Chat.Id, "Номер в телеграм и в личном кабинете сайта отличаются.");
                return false;

            case AuthenticateTelegramUserStatus.Success:

                await BotClient.SendTextMessageAsync(message.Chat.Id, "Телеграм аккаунт успешно аутентифицирован.");
                break;
        }

        return true;
    }

    private async Task<RegisterBotContactStatus> GetRegisterBotContactStatus(Message message, bool isStartCommand, CancellationToken cancellationToken)
    {
        bool isBotContactRegistered = await Context.TelegramBotContacts
            .AnyAsync(contact => contact.TelegramUserId == message.From.Id, cancellationToken);

        if (isBotContactRegistered)
        {
            return RegisterBotContactStatus.Ready;
        }

        if (!isStartCommand)
        {
            return RegisterBotContactStatus.Invalid;
        }

        return await RegisterTelegramBotContact(message, cancellationToken);
    }

    private async Task<AuthenticateTelegramUserStatus> GetAuthenticateTelgramUserStatus(Message message, CancellationToken cancellationToken)
    {
        bool isAuthenticated = await Context.TelegramBotContacts
            .Where(contact => contact.TelegramUserId == message.From.Id)
            .Select(contact => contact.IsAuthenticated)
            .SingleOrDefaultAsync(cancellationToken);

        if (isAuthenticated)
        {
            return AuthenticateTelegramUserStatus.Ready;
        }

        if (message.Contact == null)
        {
            return AuthenticateTelegramUserStatus.HasNoPhone;
        }

        return await AuthenticateTelegramUser(message, cancellationToken);
    }

    private async Task<RegisterBotContactStatus> RegisterTelegramBotContact(Message message, CancellationToken cancellationToken)
    {
        var telegramBotAccessToken = message.Text?.Remove(0, Commands.Start.Length + 1);

        int userId = 0;

        try
        {
            userId = JwtParser.FromToken(telegramBotAccessToken);
        }
        catch (Exception exception)
        {
            Logger.LogError(exception, "Error of getting telegram bot access token.");

            return RegisterBotContactStatus.Invalid;
        }

        var telegramBot = new TelegramBotContact
        {
            TelegramUserId = message.From.Id,
            UserId = userId
        };

        Context.Add(telegramBot);
        await Context.SaveChangesAsync(cancellationToken);

        return RegisterBotContactStatus.Success;
    }

    private async Task<AuthenticateTelegramUserStatus> AuthenticateTelegramUser(Message message, CancellationToken cancellationToken)
    {
        var phoneNumber = message.Contact?.PhoneNumber;

        var userByTelegramId = await Context.Users
            .Include(user => user.TelegramBotContact)
            .Where(user => user.TelegramBotContact.TelegramUserId == message.From.Id)
            .SingleAsync(cancellationToken);

        if (userByTelegramId.PhoneNumber != phoneNumber)
        {
            return AuthenticateTelegramUserStatus.DifferentPhones;
        }

        userByTelegramId.TelegramBotContact.IsAuthenticated = true;

        await Context.SaveChangesAsync(cancellationToken);

        return AuthenticateTelegramUserStatus.Success;
    }
}


