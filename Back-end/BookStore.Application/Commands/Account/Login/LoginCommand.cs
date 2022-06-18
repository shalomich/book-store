using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Application.Exceptions;
using BookStore.Application.Commands.Account.Common;

namespace BookStore.Application.Commands.Account.Login;
public record LoginCommand(LoginDto LoginForm) : IRequest<TokensDto>;
internal class LoginCommandHandler : IRequestHandler<LoginCommand, TokensDto>
{
    private UserManager<User> UserManager { get; }
    private SignInManager<User> SignInManager { get; }
    private TokensFactory TokensFactory { get; }

    public LoginCommandHandler(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        TokensFactory tokensFactory)
    {
        UserManager = userManager;
        SignInManager = signInManager;
        TokensFactory = tokensFactory;
    }

    public async Task<TokensDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var loginForm = request.LoginForm;

        var user = await UserManager.FindByEmailAsync(loginForm.Email);

        if (user == null)
        {
            throw new NotFoundException(nameof(user));
        }

        var result = await SignInManager.CheckPasswordSignInAsync(user, loginForm.Password, false);

        if (!result.Succeeded)
        {
            throw new BadRequestException("Wrong password");
        }

        return await TokensFactory.GenerateTokens(user);
    }
}

