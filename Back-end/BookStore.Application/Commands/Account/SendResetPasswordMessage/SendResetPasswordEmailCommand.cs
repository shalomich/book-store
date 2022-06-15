using BookStore.Application.Exceptions;
using BookStore.Application.Providers;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Account.SendResetPasswordMessage;

public record SendResetPasswordMessageCommand(ForgotPasswordDto ForgotPasswordDto) : IRequest;
internal class SendResetPasswordMessageCommandHandler : AsyncRequestHandler<SendResetPasswordMessageCommand>
{
    private UserManager<User> UserManager { get; }
    private EmailService EmailService { get; }
    private ILogger<SendResetPasswordMessageCommandHandler> Logger { get; }

    public SendResetPasswordMessageCommandHandler(
        UserManager<User> userManager,
        EmailService emailService,
        ILogger<SendResetPasswordMessageCommandHandler> logger)
    {
        UserManager = userManager;
        EmailService = emailService;
        Logger = logger;
    }

    protected async override Task Handle(SendResetPasswordMessageCommand request, CancellationToken cancellationToken)
    {
        await Valdiate(request);

        var email = request.ForgotPasswordDto.Email;

        var user = await UserManager.FindByEmailAsync(email);

        var code = await UserManager.GeneratePasswordResetTokenAsync(user);

        try
        {
            await EmailService.SendEmailAsync(email, "Reset password", code, cancellationToken);
        }
        catch (Exception exception)
        {
            Logger.LogError(exception, "Fail to send reset password email to {Email}", email);

            throw;
        }
    }

    private async Task Valdiate(SendResetPasswordMessageCommand request)
    {
        var user = await UserManager.FindByEmailAsync(request.ForgotPasswordDto.Email);

        if (user == null)
        {
            throw new BadRequestException("User does not exist with this email.");
        }

        bool emailConfirmed = await UserManager.IsEmailConfirmedAsync(user);

        if (!emailConfirmed)
        {
            throw new BadRequestException("Email is not confirmed");
        }
    }
}

