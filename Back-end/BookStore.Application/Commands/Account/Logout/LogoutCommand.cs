using BookStore.Application.Commands.Account.Common;
using BookStore.Application.Exceptions;
using BookStore.Application.Extensions;
using BookStore.Application.Services.Jwt;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Account.Logout;

public record LogoutCommand(TokensDto Tokens) : IRequest;
internal class LogoutCommandHandler : AsyncRequestHandler<LogoutCommand>
{
    private RefreshTokenRepository RefreshTokenRepository { get; }
    private UserManager<User> UserManager { get; }
    private WebJwtParser JwtParser { get; }

    public LogoutCommandHandler(
        RefreshTokenRepository refreshTokenRepository,
        UserManager<User> userManager,
        WebJwtParser jwtParser)
    {
        RefreshTokenRepository = refreshTokenRepository;
        UserManager = userManager;
        JwtParser = jwtParser;
    }

    protected override async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var tokens = request.Tokens;

        int userId = JwtParser
            .FromToken(tokens.AccessToken, checkExpiration: false)
            .GetCurrentUserId();

        var user = await UserManager.FindByIdAsync(userId.ToString());

        if (user == null)
            throw new NotFoundException("Wrong access token.");

        if (!await RefreshTokenRepository.IsValid(tokens.RefreshToken, user))
        {
            throw new BadRequestException("Wrong refresh token");
        }

        await RefreshTokenRepository.Remove(user);
    }
}
