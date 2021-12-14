using BookStore.Application.Dto;
using BookStore.Application.Providers;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using BookStore.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Account
{
    internal abstract class AccountHandler
    {
        private readonly JwtConverter _jwtConverter;
        private readonly UserManager<User> _userManager;
        private readonly IJwtProvider _jwtProvider;

        protected AccountHandler(JwtConverter jwtConverter, UserManager<User> userManager, IJwtProvider jwtProvider)
        {
            _jwtConverter = jwtConverter ?? throw new ArgumentNullException(nameof(jwtConverter));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _jwtProvider = jwtProvider ?? throw new ArgumentNullException(nameof(jwtProvider));
        }

        protected async Task<TokensDto> GenerateTokens(User user, UserRole role)
        {
            string appTokenProvider = _jwtProvider.GenerateSettings().AppTokenProvider;

            string newRefreshToken = await _userManager.GenerateUserTokenAsync(user, appTokenProvider, nameof(TokensDto.RefreshToken));
            await _userManager.SetAuthenticationTokenAsync(user, appTokenProvider, nameof(TokensDto.RefreshToken), newRefreshToken);

            var authorizedData = new AuthorizedDataDto(user.Id, user.Email, role);
            string newAccessToken = _jwtConverter.ToToken(authorizedData);

            return new TokensDto(newAccessToken, newRefreshToken);
        }
    }
}
