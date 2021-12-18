using BookStore.Application.Dto;
using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services
{
    public class TokensFactory
    {
        private JwtParser JwtParser { get; }
        private RefreshTokenRepository RefreshTokenRepository { get; }

        public TokensFactory(JwtParser jwtParser, RefreshTokenRepository refreshTokenRepository)
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
}
