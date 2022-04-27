using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Application.Services;
using BookStore.Application.Exceptions;
using BookStore.Application.Dto;

namespace BookStore.Application.Commands.Account
{
	public record RefreshTokenCommand(string RefreshToken) : IRequest<TokensDto>;
	internal class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, TokensDto>
	{
		private const string WrongRefreshTokenMessage = "Wrong refresh token";

		private RefreshTokenRepository RefreshTokenRepository { get; }
        private TokensFactory TokensFactory { get; }
		private LoggedUserAccessor LoggedUserAccessor { get; }
		private UserManager<User> UserManager { get; }

        public RefreshTokenHandler(RefreshTokenRepository refreshTokenRepository, TokensFactory tokensFactory, LoggedUserAccessor loggedUserAccessor, UserManager<User> userManager)
        {
            RefreshTokenRepository = refreshTokenRepository;
            TokensFactory = tokensFactory;
            LoggedUserAccessor = loggedUserAccessor;
            UserManager = userManager;
        }

        public async Task<TokensDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
		{
            int userId = LoggedUserAccessor.GetCurrentUserId();

            var user = await UserManager.FindByIdAsync(userId.ToString());

            if (user == null)
                throw new NotFoundException("Wrong access token.");
            
			if (await RefreshTokenRepository.IsValid(request.RefreshToken, user) == false)
            {
				await RefreshTokenRepository.Remove(user);
				throw new BadRequestException(WrongRefreshTokenMessage);
			}
			
			return await TokensFactory.GenerateTokens(user);
		}
	}
}
