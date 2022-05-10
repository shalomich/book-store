using BookStore.Application.Services.Jwt;
using BookStore.Domain.Entities;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BookStore.TelegramBot.UseCases.RegisterTelegramBotContact;

public record RegisterTelegramBotContactCommand(Message Message) : IRequest;
internal class AuthorizeTelegramBotHandler : AsyncRequestHandler<RegisterTelegramBotContactCommand>
{
    private ApplicationContext Context { get; }
    private ITelegramBotClient BotClient { get; }
    private TelegramBotJwtParser JwtParser { get; }
    private ILogger Logger { get; }

    public AuthorizeTelegramBotHandler(ApplicationContext context, ITelegramBotClient botClient, 
        TelegramBotJwtParser jwtParser, ILogger<AuthorizeTelegramBotHandler> logger)
    {
        Context = context;
        BotClient = botClient;
        JwtParser = jwtParser;
        Logger = logger;
    }

    protected override async Task Handle(RegisterTelegramBotContactCommand request, CancellationToken cancellationToken)
    {
        var message = request.Message;

        const string command = "/start";

        var telegramBotAccessToken = message.Text?.Remove(0, command.Length - 1);

        int userId = 0;

        try
        {
            userId = JwtParser.FromToken(telegramBotAccessToken);
        }
        catch(Exception exception)
        {
            Logger.LogError(exception, "Error of getting telegram bot access token.");

            await BotClient.SendTextMessageAsync(message.Chat.Id, "Необходимо зарегистрироваться через сайт по ссылке: ");

            return;
        }

        bool isBotRegistered = await Context.TelegramBotContacts
            .AnyAsync(bot => bot.UserId == userId);

        if (isBotRegistered)
        {
            Logger.LogError("Bot has already been registered.");

            await BotClient.SendTextMessageAsync(message.Chat.Id, "Бот уже зарегистрирован");

            return;
        }

        var telegramBot = new TelegramBotContact
        {
            TelegramUserId = message.From.Id,
            UserId = userId
        };

        Context.Add(telegramBot);
        await Context.SaveChangesAsync(cancellationToken);

        await BotClient.SendTextMessageAsync(message.Chat.Id, "Бот успешно зарегистрирован. Для аутентификации вашего аккаунта в онлайн-магазине нам нужен ваш номер телефона.");
    }
}


