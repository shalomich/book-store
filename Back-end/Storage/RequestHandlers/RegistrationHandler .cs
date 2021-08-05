using Auth.Exceptions;
using Auth.Models;
using Auth.Services;
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
using static Auth.Registration.RegistrationHandler;

namespace Auth.Registration
{
	public class RegistrationHandler : IRequestHandler<RegistrationCommand, AuthAnswer>
	{
		public record RegistrationCommand(string UserName, string Email, string Password) : IRequest<AuthAnswer>;

		private const string _defaultUserRole = "customer";

		private static readonly RestError _emailExist = new RestError { Reason = "EmailExist", Message = "Current email belongs to other" };
		private static readonly RestError _userNameExist = new RestError { Reason = "UserNameExist", Message = "Current userName belongs to other" };
		
		private JwtGenerator _jwtGenerator;
		private readonly UserManager<User> _userManager;
		
        public RegistrationHandler(JwtGenerator jwtGenerator, UserManager<User> userManager)
        {
            _jwtGenerator = jwtGenerator ?? throw new ArgumentNullException(nameof(jwtGenerator));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<AuthAnswer> Handle(RegistrationCommand request, CancellationToken cancellationToken)
		{
			var errors = new List<RestError>();

			var (userName, email, password) = request;

			if (await _userManager.Users.Where(x => x.Email == email).AnyAsync())
				errors.Add(_emailExist);

			if (await _userManager.Users.Where(x => x.UserName == userName).AnyAsync())
				errors.Add(_userNameExist);


			User user = null;

            try
            {
				user = new User
				{
					Email = email,
					UserName = userName
				};
			}
			catch (ArgumentException exception)
            {
				errors.Add(new RestError { Reason = "InvalidAccountData",Message = exception.Message});
            }

			if (errors.Count != 0)
				throw new RestException(HttpStatusCode.BadRequest, errors);


			var result = await _userManager.CreateAsync(user, password);

			
			if (result.Succeeded)
			{
				await _userManager.AddToRoleAsync(user, _defaultUserRole);

				string token = _jwtGenerator.CreateToken(user, _defaultUserRole);

				return new AuthAnswer { Token = token};
			}
			else
			{ 
				errors = result.Errors.Select(error => new RestError {Reason = error.Code, Message = error.Description}).ToList();
				throw new RestException(HttpStatusCode.BadRequest, errors);
			}

		}
	}
}
