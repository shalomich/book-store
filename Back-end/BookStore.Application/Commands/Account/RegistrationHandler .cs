
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Application.ViewModels.Account;
using BookStore.Application.Services;
using BookStore.Application.Exceptions;
using BookStore.Domain.Enums;
using BookStore.Application.Dto;
using BookStore.Application.Providers;

namespace BookStore.Application.Commands.Account
{
	public record RegistrationCommand(string Email, string FirstName, string Password) : IRequest<TokensDto>;
	internal class RegistrationHandler : IRequestHandler<RegistrationCommand, TokensDto>
	{ 
		private UserManager<User> UserManager { get; }
		private TokensFactory TokensFactory { get; }

        public RegistrationHandler(UserManager<User> userManager, TokensFactory tokensFactory)
        {
            UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            TokensFactory = tokensFactory ?? throw new ArgumentNullException(nameof(tokensFactory));
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

			return await TokensFactory.GenerateTokens(user);
        }
	}
}
