using BookStore.Application.Exceptions;
using BookStore.Application.Services;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.TelegramBot.RemoveTelegramBotContact;

public record RemoveTelegramBotContactCommand() : IRequest;
internal class RemoveTelegramBotContactCommandHandler : AsyncRequestHandler<RemoveTelegramBotContactCommand>
{
    private LoggedUserAccessor LoggedUserAccessor { get;}
    private ApplicationContext Context { get; }

    public RemoveTelegramBotContactCommandHandler(
        LoggedUserAccessor loggedUserAccessor, 
        ApplicationContext context)
    {
        LoggedUserAccessor = loggedUserAccessor;
        Context = context;
    }

    protected override async Task Handle(RemoveTelegramBotContactCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = LoggedUserAccessor.GetCurrentUserId();

        var telegramBotContact = await Context.TelegramBotContacts
            .SingleOrDefaultAsync(contact => contact.UserId == currentUserId, cancellationToken);

        if (telegramBotContact == null)
        {
            throw new NotFoundException(nameof(TelegramBot));
        }

        Context.TelegramBotContacts.Remove(telegramBotContact);

        await Context.SaveChangesAsync(cancellationToken);
    }
}

