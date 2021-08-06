using App.Areas.Auth.Services;
using App.Areas.Auth.ViewModels;
using App.Entities;
using Auth.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using static App.Areas.Auth.RequestHandlers.LoginHandler;

namespace App.Areas.Auth.RequestHandlers
{
	public class LoginHandler : IRequestHandler<LoginCommand, AuthorizedData>
	{
		public record LoginCommand(AuthForm AuthForm) : IRequest<AuthorizedData>;
		
		private static readonly RestError _emailUnregistered = new RestError { Reason = "EmailUnregistered", Message = "This email is unregistered" };
		private static readonly RestError _passwordUncorrect = new RestError { Reason = "PasswordUncorrect", Message = "Wrong password" };

		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private JwtGenerator _jwtGenerator;

        public LoginHandler(UserManager<User> userManager, SignInManager<User> signInManager, JwtGenerator jwtGenerator)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _jwtGenerator = jwtGenerator ?? throw new ArgumentNullException(nameof(jwtGenerator));
        }

        public async Task<AuthorizedData> Handle(LoginCommand request, CancellationToken cancellationToken)
		{
			var (email, password) = request.AuthForm;

			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
				throw new RestException(HttpStatusCode.BadRequest, new List<RestError>() { _emailUnregistered});
			

			var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

			if (result.Succeeded)
			{
				string role = (await _userManager.GetRolesAsync(user)).First();
				string token = _jwtGenerator.CreateToken(user, role);
				return new AuthorizedData(token, role);
			}
			else throw new RestException(HttpStatusCode.BadRequest, new List<RestError>() { _passwordUncorrect});
			
		}
	}
}
