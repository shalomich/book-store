using App.Areas.Auth.Services;
using App.Areas.Auth.ViewModels;
using App.Entities;
using App.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using static App.Areas.Auth.RequestHandlers.RegistrationHandler;

namespace App.Areas.Auth.RequestHandlers
{
	public class RegistrationHandler : IRequestHandler<RegistrationCommand, AuthorizedData>
	{
		public record RegistrationCommand(AuthForm AuthForm) : IRequest<AuthorizedData>;

		private const string DefaultUserRole = "customer";
		private const string EmailTakenMessage = "This email is already taken";

		private JwtGenerator _jwtGenerator;
		private readonly UserManager<User> _userManager;
		
        public RegistrationHandler(JwtGenerator jwtGenerator, UserManager<User> userManager)
        {
            _jwtGenerator = jwtGenerator ?? throw new ArgumentNullException(nameof(jwtGenerator));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<AuthorizedData> Handle(RegistrationCommand request, CancellationToken cancellationToken)
		{
			var (email, password) = request.AuthForm;

			if (_userManager.Users.Any(user => user.Email == email))
				throw new BadRequestException(EmailTakenMessage);

			var user = new User { Email = email, UserName = email };

			var result = await _userManager.CreateAsync(user, password);
			
			if (result.Succeeded)
			{
				await _userManager.AddToRoleAsync(user, DefaultUserRole);

				string token = _jwtGenerator.CreateToken(user, DefaultUserRole);

				return new AuthorizedData(token, DefaultUserRole);
			}
			else
			{
				var passwordErrorMessage = result.Errors.First().Description;
				throw new BadRequestException(passwordErrorMessage);
			}

		}
	}
}
