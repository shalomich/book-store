using Auth.Exceptions;
using Auth.Models;
using Auth.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Login
{
	public class LoginHandler : IRequestHandler<LoginQuery, AuthAnswer>
	{
		private static readonly RestError _emailUnregistered = new RestError { Reason = "EmailUnregistered", Message = "This email is unregistered" };
		private static readonly RestError _passwordUncorrect = new RestError { Reason = "PasswordUncorrect", Message = "Wrong password" };

		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private IJwtGenerator _jwtGenerator;

        public LoginHandler(UserManager<User> userManager, SignInManager<User> signInManager, IJwtGenerator jwtGenerator)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _jwtGenerator = jwtGenerator ?? throw new ArgumentNullException(nameof(jwtGenerator));
        }

        public async Task<AuthAnswer> Handle(LoginQuery request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByEmailAsync(request.Email);
			if (user == null)
				throw new RestException(HttpStatusCode.BadRequest, new List<RestError>() { _emailUnregistered});
			

			var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

			if (result.Succeeded)
			{
				string role = (await _userManager.GetRolesAsync(user)).First();
				return new AuthAnswer
				{
					Token = _jwtGenerator.CreateToken(user,role),
				};
			}
			else throw new RestException(HttpStatusCode.BadRequest, new List<RestError>() { _passwordUncorrect});
			
		}
	}
}
