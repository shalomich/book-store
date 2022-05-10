using BookStore.Application.Services.Jwt;
using BookStore.Domain.Entities;
using BookStore.Persistance;
using BookStore.TelegramBot.UseCases.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BookStore.TelegramBot.UseCases.TryRegisterBotContact;

public record TryRegisterBotContactCommand(Message Message, bool IsStartCommand) : IRequest<RegisterBotContactStatus>;
internal class TryRegisterBotContactCommandHandler : IRequestHandler<TryRegisterBotContactCommand, RegisterBotContactStatus>
{
    private ApplicationContext Context { get; }
    private TelegramBotJwtParser JwtParser { get; }
    private ILogger Logger { get; }

    public TryRegisterBotContactCommandHandler(ApplicationContext context, ITelegramBotClient botClient,
        TelegramBotJwtParser jwtParser, ILogger<TryRegisterBotContactCommandHandler> logger)
    {
        Context = context;
        JwtParser = jwtParser;
        Logger = logger;
    }

    public async Task<RegisterBotContactStatus> Handle(TryRegisterBotContactCommand request, CancellationToken cancellationToken)
    {
        var (message, isStartCommand) = request;

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

        int userId = 0;

        try
        {
            var telegramBotAccessToken = message.Text?.Split(" ")[1];

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
}


