using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Application.Services;
using BookStore.Application.Exceptions;
using BookStore.Application.Dto;
using BookStore.Domain.Enums;

namespace BookStore.Application.Commands.Account;
public record RegistrationCommand(string Email, string FirstName, string Password) : IRequest<TokensDto>;
internal class RegistrationHandler : IRequestHandler<RegistrationCommand, TokensDto>
{
	private UserManager<User> UserManager { get; }
	private TokensFactory TokensFactory { get; }

    public RegistrationHandler(
		UserManager<User> userManager, 
		TokensFactory tokensFactory)
    {
        UserManager = userManager;
        TokensFactory = tokensFactory;
    }

    public async Task<TokensDto> Handle(RegistrationCommand request, CancellationToken cancellationToken)
	{
		var (email, fistName, password) = request;

		var user = new User 
		{ 
			Email = email, 
			UserName = email,
			FirstName = fistName
		};

		var result = await UserManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            var errorMessage = result.Errors.First().Description;
            throw new BadRequestException(errorMessage);
		}

		await UserManager.AddToRoleAsync(user, RoleName.Customer.ToString());

		return await TokensFactory.GenerateTokens(user);
    }
}