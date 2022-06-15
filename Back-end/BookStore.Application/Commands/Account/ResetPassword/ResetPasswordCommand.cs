using BookStore.Application.Exceptions;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Account.ResetPassword;

public record ResetPasswordCommand(ResetPasswordDto ResetPasswordDto) : IRequest;
internal class ResetPasswordCommandHandler : AsyncRequestHandler<ResetPasswordCommand>
{
    private UserManager<User> UserManager { get; }

    public ResetPasswordCommandHandler(UserManager<User> userManager)
    {
        UserManager = userManager;
    }

    protected async override Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var resetPasswordDto = request.ResetPasswordDto;

        var user = await UserManager.FindByEmailAsync(resetPasswordDto.Email);

        if (user == null)
        {
            throw new BadRequestException("User does not exist with this email.");
        }

        var result = await UserManager.ResetPasswordAsync(user, resetPasswordDto.Code, resetPasswordDto.Password);

        if (!result.Succeeded)
        {
            var errorMessage = result.Errors.First().Description;
            throw new BadRequestException(errorMessage);
        }
    }
}

