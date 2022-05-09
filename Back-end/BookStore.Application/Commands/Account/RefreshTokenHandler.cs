using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Application.Services;
using BookStore.Application.Exceptions;
using BookStore.Application.Dto;
using BookStore.Application.Services.Jwt;

namespace BookStore.Application.Commands.Account
{
	public record RefreshTokenCommand(TokensDto PreviousTokens) : IRequest<TokensDto>;
	internal class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, TokensDto>
	{
		private RefreshTokenRepository RefreshTokenRepository { get; }
        private TokensFactory TokensFactory { get; }
		private UserManager<User> UserManager { get; }
		private WebJwtParser JwtParser { get; }

        public RefreshTokenHandler(RefreshTokenRepository refreshTokenRepository, TokensFactory tokensFactory,
			WebJwtParser jwtParser, UserManager<User> userManager)
        {
            RefreshTokenRepository = refreshTokenRepository;
            TokensFactory = tokensFactory;
			UserManager = userManager;
			JwtParser = jwtParser;
        }

        public async Task<TokensDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
		{
			var (accessToken, refreshToken) = request.PreviousTokens;
            
			int userId = JwtParser.FromToken(accessToken,checkExpiration: false);

            var user = await UserManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
				throw new BadRequestException("Wrong access token. You need to login in system.");
			}

			if (await RefreshTokenRepository.IsValid(refreshToken, user) == false)
            {
				await RefreshTokenRepository.Remove(user);
				throw new BadRequestException("Wrong refresh token. You need to login in system.");
			}
			
			return await TokensFactory.GenerateTokens(user);
		}
	}
}
