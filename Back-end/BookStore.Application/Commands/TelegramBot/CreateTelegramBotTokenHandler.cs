using BookStore.Application.Services;
using BookStore.Application.Services.Jwt;
using BookStore.Persistance;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.TelegramBot;

public record CreateTelegramBotTokenCommand() : IRequest<string>;
internal class CreateTelegramBotTokenHandler : IRequestHandler<CreateTelegramBotTokenCommand, string>
{
    private LoggedUserAccessor LoggedUserAccessor { get; }
    private TelegramBotJwtParser JwtParser { get; }

    public CreateTelegramBotTokenHandler(LoggedUserAccessor loggedUserAccessor, TelegramBotJwtParser jwtParser)
    {
        LoggedUserAccessor = loggedUserAccessor;
        JwtParser = jwtParser;
    }

    public Task<string> Handle(CreateTelegramBotTokenCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = LoggedUserAccessor.GetCurrentUserId();

        var telegramAccessToken = JwtParser.ToToken(currentUserId);

        return Task.FromResult(telegramAccessToken);
    }
}

