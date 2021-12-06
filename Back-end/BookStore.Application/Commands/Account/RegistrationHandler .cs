
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

namespace BookStore.Application.Commands.Account
{
	public record RegistrationCommand(AuthForm AuthForm) : IRequest<string>;
	public class RegistrationHandler : IRequestHandler<RegistrationCommand, string>
	{ 
		private const string DefaultUserRole = "customer";
		private const string EmailTakenMessage = "This email is already taken";

		private JwtGenerator _jwtGenerator;
		private readonly UserManager<User> _userManager;
		
        public RegistrationHandler(JwtGenerator jwtGenerator, UserManager<User> userManager)
        {
            _jwtGenerator = jwtGenerator ?? throw new ArgumentNullException(nameof(jwtGenerator));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<string> Handle(RegistrationCommand request, CancellationToken cancellationToken)
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

            await _userManager.AddToRoleAsync(user, DefaultUserRole);

			return _jwtGenerator.CreateToken(user, DefaultUserRole);
        }
	}
}
