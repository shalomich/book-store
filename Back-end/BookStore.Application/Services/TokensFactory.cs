using BookStore.Application.Dto;
using BookStore.Application.Services.Jwt;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace BookStore.Application.Services;
internal class TokensFactory
{
    private WebJwtParser JwtParser { get; }
    private RefreshTokenRepository RefreshTokenRepository { get; }
    private SignInManager<User> SignInManager { get; }

    public TokensFactory(
        WebJwtParser jwtParser, 
        RefreshTokenRepository refreshTokenRepository,
        SignInManager<User> signInManager)
    {
        JwtParser = jwtParser;
        RefreshTokenRepository = refreshTokenRepository;
        SignInManager = signInManager;
    }

    public async Task<TokensDto> GenerateTokens(User user)
    {
        var principal = await SignInManager.CreateUserPrincipalAsync(user);

        return new TokensDto(
            AccessToken: JwtParser.ToToken(principal),
            RefreshToken: await RefreshTokenRepository.Create(user));
    }
}

