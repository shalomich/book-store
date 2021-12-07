
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Application.ViewModels.Account;
using BookStore.Application.Services;
using BookStore.Application.Exceptions;
using BookStore.Application.Dto;
using BookStore.Domain.Enums;
using BookStore.Application.Providers;

namespace BookStore.Application.Commands.Account
{
	public record LoginCommand(AuthForm AuthForm) : IRequest<TokensDto>;
	internal class LoginHandler : AccountHandler, IRequestHandler<LoginCommand, TokensDto>
	{
		private const string NotExistEmailMessage = "This email does not exist";
		private const string WrongPasswordMessage = "Wrong password";

		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private JwtConverter _jwtConverter;

        public LoginHandler(UserManager<User> userManager, SignInManager<User> signInManager, JwtConverter jwtConverter, IJwtProvider jwtProvider)
			:base(jwtConverter, userManager, jwtProvider)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _jwtConverter = jwtConverter ?? throw new ArgumentNullException(nameof(jwtConverter));
        }

        public async Task<TokensDto> Handle(LoginCommand request, CancellationToken cancellationToken)
		{
			var (email, password) = request.AuthForm;

			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
				throw new BadRequestException(NotExistEmailMessage);


			var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

			if (!result.Succeeded)
                throw new BadRequestException(WrongPasswordMessage);

			var role = Enum.Parse<UserRole>((await _userManager
				.GetRolesAsync(user))
				.Single());

			return await GenerateTokens(user, role);
        }
	}
}
