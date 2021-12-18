using BookStore.Application.Dto;
using BookStore.Application.Exceptions;
using BookStore.Application.Providers;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Account;

public record LogoutCommand(TokensDto Tokens) : IRequest<Unit>;
internal class LogoutHandler : IRequestHandler<LogoutCommand, Unit>
{
	private const string NotExistEmailMessage = "This email does not exist";
	private const string WrongRefreshTokenMessage = "Wrong refresh token";

	private readonly UserManager<User> _userManager;
	private readonly JwtConverter _jwtConverter;
	private readonly IJwtProvider _jwtProvider;

	public LogoutHandler(UserManager<User> userManager, JwtConverter jwtConverter, IJwtProvider jwtProvider)
	{
		_userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
		_jwtConverter = jwtConverter ?? throw new ArgumentNullException(nameof(jwtConverter));
		_jwtProvider = jwtProvider ?? throw new ArgumentNullException(nameof(jwtProvider));
	}

	public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
	{
		var (accessToken, refreshToken) = request.Tokens;

		AuthorizedDataDto authorizedData;

		try
        {
			authorizedData = _jwtConverter.FromToken(accessToken);
		}
		catch (Exception exception)
        {
			throw new BadRequestException(null, exception);
        }
		
		var user = await _userManager.FindByEmailAsync(authorizedData.Email);
		if (user == null)
			throw new BadRequestException(NotExistEmailMessage);

		string appTokenProvider = _jwtProvider.GenerateSettings().AppTokenProvider;
		var userRefreshToken = await _userManager.GetAuthenticationTokenAsync(user, appTokenProvider, nameof(TokensDto.RefreshToken));

		if (userRefreshToken != refreshToken)
			throw new BadRequestException(WrongRefreshTokenMessage);

		await _userManager.RemoveAuthenticationTokenAsync(user, appTokenProvider, nameof(TokensDto.RefreshToken));

		return Unit.Value;
	}
}
