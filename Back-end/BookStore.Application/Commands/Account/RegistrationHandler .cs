
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
	public record RegistrationCommand(AuthForm AuthForm) : IRequest<TokensDto>;
	internal class RegistrationHandler : AccountHandler, IRequestHandler<RegistrationCommand, TokensDto>
	{ 
		private const UserRole DefaultRole = UserRole.Customer;
		private const string EmailTakenMessage = "This email is already taken";

		private JwtConverter _jwtConverter;
		private readonly UserManager<User> _userManager;

        public RegistrationHandler(JwtConverter jwtConverter, UserManager<User> userManager, IJwtProvider jwtProvider) 
			: base(jwtConverter, userManager, jwtProvider)
        {
            _jwtConverter = jwtConverter ?? throw new ArgumentNullException(nameof(jwtConverter));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<TokensDto> Handle(RegistrationCommand request, CancellationToken cancellationToken)
		{
			var (email, password) = request.AuthForm;

			if (_userManager.Users.Any(user => user.Email == email))
				throw new BadRequestException(EmailTakenMessage);

			var user = new User { Email = email, UserName = email };

			var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                var passwordErrorMessage = result.Errors.First().Description;
                throw new BadRequestException(passwordErrorMessage);
			}

            await _userManager.AddToRoleAsync(user, DefaultRole.ToString());

			return await GenerateTokens(user, DefaultRole);
        }
	}
}
