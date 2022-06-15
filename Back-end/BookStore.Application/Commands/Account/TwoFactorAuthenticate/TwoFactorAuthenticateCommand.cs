using BookStore.Application.Exceptions;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Account.TwoFactorAuthenticate;

public record TwoFactorAuthenticateCommand(TwoFactorAuthenticateDto TwoFactorAuthenticateDto) : IRequest;
internal class TwoFactorAuthenticateCommandHandler : AsyncRequestHandler<TwoFactorAuthenticateCommand>
{
    private string TokenProdiver { get; }
    private LoggedUserAccessor LoggedUserAccessor { get; }
    private SignInManager<User> SignInManager { get; }

    public TwoFactorAuthenticateCommandHandler(
        LoggedUserAccessor loggedUserAccessor,
        SignInManager<User> signInManager,
        IConfiguration configuration)
    {
        LoggedUserAccessor = loggedUserAccessor;
        SignInManager = signInManager;

        TokenProdiver = configuration["Auth:AppTokenProvider"];
    }

    protected async override Task Handle(TwoFactorAuthenticateCommand request, CancellationToken cancellationToken)
    {
        var result = await SignInManager.TwoFactorSignInAsync(TokenProdiver, request.TwoFactorAuthenticateDto.Code,
            false, false);

        if (!result.Succeeded)
        {
            throw new BadRequestException("Invalid two factor auth code.");
        }
    }
}

