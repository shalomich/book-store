
using BookStore.Application.Commands.Account.CheckEmailExistence;
using BookStore.Application.Commands.Account.Common;
using BookStore.Application.Commands.Account.Login;
using BookStore.Application.Commands.Account.Logout;
using BookStore.Application.Commands.Account.Registration;
using BookStore.Application.Commands.Account.ResfreshToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.WebApi.Areas.Store.Controllers;
[ApiController]
[Area("store")]
[Route("[area]/[controller]")]
public class AccountController : ControllerBase
{
    private IMediator Mediator { get; }

    public AccountController(IMediator mediator)
    {
        Mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<TokensDto> Login(LoginDto loginForm, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new LoginCommand(loginForm), cancellationToken);
    }

    [HttpPost("registration")]
    public Task<TokensDto> Registration(RegistrationDto registrationForm, CancellationToken cancellationToken)
    {
        return Mediator.Send(new RegistrationCommand(registrationForm), cancellationToken);
    }

    [HttpPost("logout")]
    public async Task Logout(TokensDto tokens, CancellationToken cancellationToken)
    {
        await Mediator.Send(new LogoutCommand(tokens), cancellationToken);
    }

    [HttpPost("refresh")]
    public async Task<TokensDto> RefreshToken(TokensDto tokens, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new RefreshTokenCommand(tokens), cancellationToken);
    }

    [HttpGet("email-existence/{email}")]
    public async Task<bool> CheckEmailExistence(string email, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new CheckEmailExistenceQuery(email), cancellationToken);
    }
}

