using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;

namespace BookStore.TelegramBot.UseCases.TryAuthenticateTelegramUser;

public record TryAuthenticateTelegramUserCommand(Message Message) : IRequest<AuthenticateTelegramUserStatus>;
internal class TryAuthenticateTelegramUserCommandHandler : IRequestHandler<TryAuthenticateTelegramUserCommand, AuthenticateTelegramUserStatus>
{
    private ApplicationContext Context { get; }
 
    public TryAuthenticateTelegramUserCommandHandler(ApplicationContext context)
    {
        Context = context;
    }

    public async Task<AuthenticateTelegramUserStatus> Handle(TryAuthenticateTelegramUserCommand request, CancellationToken cancellationToken)
    {
        var message = request.Message;

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


