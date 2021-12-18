using BookStore.Application.Dto;
using BookStore.Application.Exceptions;
using BookStore.Application.Providers;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Account;

public record LogoutCommand(User User, string RefreshToken) : IRequest<Unit>;
internal class LogoutHandler : IRequestHandler<LogoutCommand, Unit>
{
	private const string WrongRefreshTokenMessage = "Wrong refresh token";

	private RefreshTokenRepository RefreshTokenRepository { get; }
    public LogoutHandler(RefreshTokenRepository refreshTokenRepository, UserManager<User> userManager)
    {
        RefreshTokenRepository = refreshTokenRepository ?? throw new ArgumentNullException(nameof(refreshTokenRepository));
    }

    public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
	{
		var (user, refreshToken) = request;

		if (await RefreshTokenRepository.IsValid(refreshToken, user) == false)
			throw new BadRequestException(WrongRefreshTokenMessage);

		await RefreshTokenRepository.Remove(user);
		
		return Unit.Value;
	}
}
