using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Application.Exceptions;
using BookStore.Domain.Enums;
using BookStore.Application.Commands.Account.Common;
using BookStore.Application.Notifications.UserRegistered;
using Microsoft.Extensions.Logging;

namespace BookStore.Application.Commands.Account.Registration;
public record RegistrationCommand(RegistrationDto RegistrationForm) : IRequest<TokensDto>;
internal class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, TokensDto>
{
    private UserManager<User> UserManager { get; }
    private TokensFactory TokensFactory { get; }
    private IMediator Mediator { get; }
    private ILogger<RegistrationCommandHandler> Logger { get; }

    public RegistrationCommandHandler(
        UserManager<User> userManager,
        TokensFactory tokensFactory,
        IMediator mediator,
        ILogger<RegistrationCommandHandler> logger)
    {
        UserManager = userManager;
        TokensFactory = tokensFactory;
        Mediator = mediator;
        Logger = logger;
    }

    public async Task<TokensDto> Handle(RegistrationCommand request, CancellationToken cancellationToken)
    {
        var registrationForm = request.RegistrationForm;

        var user = new User
        {
            Email = registrationForm.Email,
            UserName = registrationForm.Email,
            FirstName = registrationForm.FirstName
        };

        var result = await UserManager.CreateAsync(user, registrationForm.Password);

        if (!result.Succeeded)
        {
            var errorMessage = result.Errors.First().Description;
            throw new BadRequestException(errorMessage);
        }

        await UserManager.AddToRoleAsync(user, RoleName.Customer.ToString());

        Logger.LogInformation("User with {Email} is registered.", user.Email);

        await Mediator.Publish(new UserRegisteredNotification(user.Id), cancellationToken);

        return await TokensFactory.GenerateTokens(user);
    }
}