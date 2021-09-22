﻿using App.Areas.Store.Services;
using App.Areas.Store.ViewModels;
using App.Areas.Store.ViewModels.Authorization;
using BookStore.Domain.Entities;
using App.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using static App.Areas.Store.RequestHandlers.LoginHandler;

namespace App.Areas.Store.RequestHandlers
{
	public class LoginHandler : IRequestHandler<LoginCommand, AuthorizedData>
	{
		public record LoginCommand(AuthForm AuthForm) : IRequest<AuthorizedData>;

		private const string NotExistEmailMessage = "This email does not exist";
		private const string WrongPasswordMessage = "Wrong password";

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
				throw new BadRequestException(NotExistEmailMessage);


			var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

			if (result.Succeeded)
			{
				string role = (await _userManager.GetRolesAsync(user)).First();
				string token = _jwtGenerator.CreateToken(user, role);
				return new AuthorizedData(token, role);
			}
			else throw new BadRequestException(WrongPasswordMessage);

		}
	}
}
