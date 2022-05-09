using BookStore.Application.Dto;
using BookStore.Application.Services.Jwt;
using BookStore.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace BookStore.Application.Services;
internal class TokensFactory
{
    private WebJwtParser JwtParser { get; }
    private RefreshTokenRepository RefreshTokenRepository { get; }

    public TokensFactory(WebJwtParser jwtParser, RefreshTokenRepository refreshTokenRepository)
    {
        JwtParser = jwtParser ?? throw new ArgumentNullException(nameof(jwtParser));
        RefreshTokenRepository = refreshTokenRepository ?? throw new ArgumentNullException(nameof(refreshTokenRepository));
    }

    public async Task<TokensDto> GenerateTokens(User user)
    {
        return new TokensDto(
            AccessToken: JwtParser.ToToken(user.Id),
            RefreshToken: await RefreshTokenRepository.Create(user));
    }
}

