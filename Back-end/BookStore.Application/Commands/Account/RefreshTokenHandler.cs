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

namespace BookStore.Application.Commands.Account
{
	public record RefreshTokenCommand(User User, string RefreshToken) : IRequest<TokensDto>;
	internal class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, TokensDto>
	{
		private const string WrongRefreshTokenMessage = "Wrong refresh token";

		private RefreshTokenRepository RefreshTokenRepository { get; }
        private TokensFactory TokensFactory { get; }

        public RefreshTokenHandler(RefreshTokenRepository refreshTokenRepository, TokensFactory tokensFactory)
        {
            RefreshTokenRepository = refreshTokenRepository ?? throw new ArgumentNullException(nameof(refreshTokenRepository));
            TokensFactory = tokensFactory ?? throw new ArgumentNullException(nameof(tokensFactory));
        }

        public async Task<TokensDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
		{
			var (user, refreshToken) = request;

			if (await RefreshTokenRepository.IsValid(refreshToken, user) == false)
            {
				await RefreshTokenRepository.Remove(user);
				throw new BadRequestException(WrongRefreshTokenMessage);
			}
			
			return await TokensFactory.GenerateTokens(user);
		}
	}
}
