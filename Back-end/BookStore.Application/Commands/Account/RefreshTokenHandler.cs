using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Application.ViewModels.Account;
using BookStore.Application.Services;
using BookStore.Application.Exceptions;
using BookStore.Application.Dto;
using BookStore.Application.Providers;

namespace BookStore.Application.Commands.Account
{
	public record RefreshTokenCommand(TokensDto Tokens) : IRequest<TokensDto>;
	internal class RefreshTokenHandler : AccountHandler, IRequestHandler<RefreshTokenCommand, TokensDto>
	{
		private const string NotExistEmailMessage = "This email does not exist";
		private const string WrongRefreshTokenMessage = "Wrong refresh token";

		private readonly UserManager<User> _userManager;
		private readonly JwtConverter _jwtConverter;
		private readonly IJwtProvider _jwtProvider;

        public RefreshTokenHandler(UserManager<User> userManager, JwtConverter jwtConverter, IJwtProvider jwtProvider)
			:base(jwtConverter, userManager, jwtProvider)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _jwtConverter = jwtConverter ?? throw new ArgumentNullException(nameof(jwtConverter));
            _jwtProvider = jwtProvider ?? throw new ArgumentNullException(nameof(jwtProvider));
        }

        public async Task<TokensDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
		{
			var (accessToken, refreshToken) = request.Tokens;

			var authorizedData = _jwtConverter.FromToken(accessToken);

			var user = await _userManager.FindByEmailAsync(authorizedData.Email);
			if (user == null)
				throw new BadRequestException(NotExistEmailMessage);

			string appTokenProvider = _jwtProvider.GenerateSettings().AppTokenProvider; 
			var userRefreshToken = await _userManager.GetAuthenticationTokenAsync(user, appTokenProvider, nameof(TokensDto.RefreshToken));

			if (userRefreshToken != refreshToken)
				throw new BadRequestException(WrongRefreshTokenMessage);

			return await GenerateTokens(user, authorizedData.Role);
		}
	}
}
