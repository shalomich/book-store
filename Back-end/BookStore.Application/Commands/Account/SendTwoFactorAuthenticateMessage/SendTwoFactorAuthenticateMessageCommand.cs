using BookStore.Application.Services;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Account.SendTwoFactorAuthenticateMessage;

public record SendTwoFactorAuthenticateMessageCommand() : IRequest;
internal class SendTwoFactorAuthenticateMessageCommandHandler : AsyncRequestHandler<SendTwoFactorAuthenticateMessageCommand>
{
    private string TokenProdiver { get; }
    private UserManager<User> UserManager { get; }
    private EmailService EmailService { get; }
    private ILogger<SendTwoFactorAuthenticateMessageCommand> Logger { get; }
    private LoggedUserAccessor LoggedUserAccessor { get; }

    public SendTwoFactorAuthenticateMessageCommandHandler(
        UserManager<User> userManager,
        EmailService emailService,
        ILogger<SendTwoFactorAuthenticateMessageCommand> logger,
        LoggedUserAccessor loggedUserAccessor,
        IConfiguration configuration)
    {
        UserManager = userManager;
        EmailService = emailService;
        Logger = logger;
        LoggedUserAccessor = loggedUserAccessor;

        TokenProdiver = configuration["Auth:AppTokenProvider"];
    }

    protected async override Task Handle(SendTwoFactorAuthenticateMessageCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = LoggedUserAccessor.GetCurrentUserId();

        var user = await UserManager.FindByIdAsync(currentUserId.ToString());

        var code = await UserManager.GenerateTwoFactorTokenAsync(user, TokenProdiver);

        try
        {
            await EmailService.SendEmailAsync(user.Email, "Two factor authentication", code, cancellationToken);
        }
        catch (Exception exception)
        {
            Logger.LogError(exception, "Fail to send two factor authentication email to {Email}", user.Email);

            throw;
        }
    }
}

