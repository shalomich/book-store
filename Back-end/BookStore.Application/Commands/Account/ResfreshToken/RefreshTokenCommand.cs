using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Application.Exceptions;
using BookStore.Application.Services.Jwt;
using BookStore.Application.Extensions;
using BookStore.Application.Commands.Account.Common;

namespace BookStore.Application.Commands.Account.ResfreshToken;
public record RefreshTokenCommand(TokensDto Tokens) : IRequest<TokensDto>;
internal class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokensDto>
{
    private RefreshTokenRepository RefreshTokenRepository { get; }
    private TokensFactory TokensFactory { get; }
    private UserManager<User> UserManager { get; }
    private WebJwtParser JwtParser { get; }

    public RefreshTokenCommandHandler(
        RefreshTokenRepository refreshTokenRepository,
        TokensFactory tokensFactory,
        WebJwtParser jwtParser,
        UserManager<User> userManager)
    {
        RefreshTokenRepository = refreshTokenRepository;
        TokensFactory = tokensFactory;
        UserManager = userManager;
        JwtParser = jwtParser;
    }

    public async Task<TokensDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var tokens = request.Tokens;

        int userId = JwtParser
            .FromToken(tokens.AccessToken, checkExpiration: false)
            .GetCurrentUserId();

        var user = await UserManager.FindByIdAsync(userId.ToString());

        if (user == null)
        {
            throw new BadRequestException("Wrong access token. You need to login in system.");
        }

        if (await RefreshTokenRepository.IsValid(tokens.RefreshToken, user) == false)
        {
            await RefreshTokenRepository.Remove(user);
            throw new BadRequestException("Wrong refresh token. You need to login in system.");
        }

        return await TokensFactory.GenerateTokens(user);
    }
}
