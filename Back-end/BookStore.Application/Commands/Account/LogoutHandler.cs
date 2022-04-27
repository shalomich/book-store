using BookStore.Application.Exceptions;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Account;

public record LogoutCommand(string RefreshToken) : IRequest<Unit>;
internal class LogoutHandler : IRequestHandler<LogoutCommand, Unit>
{
	private const string WrongRefreshTokenMessage = "Wrong refresh token";

	private RefreshTokenRepository RefreshTokenRepository { get; }
	private LoggedUserAccessor LoggedUserAccessor { get; }
	private UserManager<User> UserManager { get; }
	public LogoutHandler(RefreshTokenRepository refreshTokenRepository, UserManager<User> userManager, 
		LoggedUserAccessor loggedUserAccessor)
    {
		RefreshTokenRepository = refreshTokenRepository;
		LoggedUserAccessor = loggedUserAccessor;
		UserManager = userManager;
    }

    public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
	{
		int userId = LoggedUserAccessor.GetCurrentUserId();

		var user = await UserManager.FindByIdAsync(userId.ToString());

		if (user == null)
			throw new NotFoundException("Wrong access token.");

		if (await RefreshTokenRepository.IsValid(request.RefreshToken, user) == false)
			throw new BadRequestException(WrongRefreshTokenMessage);

		await RefreshTokenRepository.Remove(user);
		
		return Unit.Value;
	}
}
