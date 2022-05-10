using BookStore.Application.Services.Jwt;
using BookStore.Domain.Entities;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BookStore.TelegramBot.UseCases.AuthenticateTelegramBotContact;

public record AuthenticateTelegramBotContactCommand(Message Message) : IRequest;
internal class AuthenticateTelegramBotContactHandler : AsyncRequestHandler<AuthenticateTelegramBotContactCommand>
{
    private ApplicationContext Context { get; }
    private ITelegramBotClient BotClient { get; }
    private TelegramBotJwtParser JwtParser { get; }
    private ILogger Logger { get; }

    public AuthenticateTelegramBotContactHandler(ApplicationContext context, ITelegramBotClient botClient,
        TelegramBotJwtParser jwtParser, ILogger<AuthenticateTelegramBotContactHandler> logger)
    {
        Context = context;
        BotClient = botClient;
        JwtParser = jwtParser;
        Logger = logger;
    }

    protected override async Task Handle(AuthenticateTelegramBotContactCommand request, CancellationToken cancellationToken)
    {
        var message = request.Message;

        var phoneNumber = message.Contact?.PhoneNumber;

        var userByTelegramId = await Context.Users
            .Include(user => user.TelegramBotContact)
            .Where(user => user.TelegramBotContact.TelegramUserId == message.From.Id)
            .SingleAsync(cancellationToken);

        if (userByTelegramId.PhoneNumber != phoneNumber)
        {
            await BotClient.SendTextMessageAsync(message.Chat.Id, "Номер в телеграм и в личном кабинете сайта отличаются.");

            return;
        }

        userByTelegramId.TelegramBotContact.IsAuthenticated = true;
    
        await Context.SaveChangesAsync(cancellationToken);

        await BotClient.SendTextMessageAsync(message.Chat.Id, "Бот успешно аутентифицирован.");
    }
}


