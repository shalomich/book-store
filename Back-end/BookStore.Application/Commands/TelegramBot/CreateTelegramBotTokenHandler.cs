using BookStore.Application.Services;
using BookStore.Application.Services.Jwt;
using BookStore.Persistance;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.TelegramBot;

public record CreateTelegramBotTokenCommand() : IRequest<TelegramBotTokenDto>;
internal class CreateTelegramBotTokenHandler : IRequestHandler<CreateTelegramBotTokenCommand, TelegramBotTokenDto>
{
    private LoggedUserAccessor LoggedUserAccessor { get; }
    private TelegramBotJwtParser JwtParser { get; }

    public CreateTelegramBotTokenHandler(LoggedUserAccessor loggedUserAccessor, TelegramBotJwtParser jwtParser)
    {
        LoggedUserAccessor = loggedUserAccessor;
        JwtParser = jwtParser;
    }

    public Task<TelegramBotTokenDto> Handle(CreateTelegramBotTokenCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = LoggedUserAccessor.GetCurrentUserId();

        var telegramBotTokenDto = new TelegramBotTokenDto
        {
            BotToken = JwtParser.ToToken(currentUserId)
        };

        return Task.FromResult(telegramBotTokenDto);
    }
}

