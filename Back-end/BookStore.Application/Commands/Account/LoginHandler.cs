using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Application.Services;
using BookStore.Application.Exceptions;
using BookStore.Application.Dto;

namespace BookStore.Application.Commands.Account;
public record LoginCommand(User User, string Password) : IRequest<TokensDto>;
internal class LoginHandler : IRequestHandler<LoginCommand, TokensDto>
{
	private const string WrongPasswordMessage = "Wrong password";
	private SignInManager<User> SignInManager { get; }
	private TokensFactory TokensFactory { get; }

    public LoginHandler(
		SignInManager<User> signInManager, 
		TokensFactory tokensFactory)
    {
        SignInManager = signInManager;
        TokensFactory = tokensFactory;
    }

    public async Task<TokensDto> Handle(LoginCommand request, CancellationToken cancellationToken)
	{
		var (user, password) = request;

		var result = await SignInManager.CheckPasswordSignInAsync(user, password, false);

		if (!result.Succeeded)
            throw new BadRequestException(WrongPasswordMessage);

		return await TokensFactory.GenerateTokens(user);
    }
}

