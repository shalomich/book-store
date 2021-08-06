using App.Areas.Auth.Services;
using App.Areas.Auth.ViewModels;
using Auth.Exceptions;
using Auth.Models;
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

		private const string _defaultUserRole = "customer";

		private JwtGenerator _jwtGenerator;
		private readonly UserManager<User> _userManager;
		
        public RegistrationHandler(JwtGenerator jwtGenerator, UserManager<User> userManager)
        {
            _jwtGenerator = jwtGenerator ?? throw new ArgumentNullException(nameof(jwtGenerator));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<AuthorizedData> Handle(RegistrationCommand request, CancellationToken cancellationToken)
		{
			var errors = new List<RestError>();

			var (email, password) = request.AuthForm;

			var user = new User { Email = email, UserName = email };

			var result = await _userManager.CreateAsync(user, password);
			
			if (result.Succeeded)
			{
				await _userManager.AddToRoleAsync(user, _defaultUserRole);

				string token = _jwtGenerator.CreateToken(user, _defaultUserRole);

				return new AuthorizedData(token, _defaultUserRole);
			}
			else
			{ 
				errors = result.Errors.Select(error => new RestError {Reason = error.Code, Message = error.Description}).ToList();
				throw new RestException(HttpStatusCode.BadRequest, errors);
			}

		}
	}
}
