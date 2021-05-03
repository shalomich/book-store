using Auth.Exceptions;
using Auth.Models;
using Auth.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Registration
{
	public class RegistrationHandler : IRequestHandler<RegistrationCommand, AuthAnswer>
	{
		private const string _defaultUserRole = "BUYER";

		private static readonly RestError _emailExist = new RestError { Reason = "EmailExist", Message = "Current email belongs to other" };
		private static readonly RestError _userNameExist = new RestError { Reason = "UserNameExist", Message = "Current userName belongs to other" };
		
		private IJwtGenerator _jwtGenerator;
		private readonly UserManager<User> _userManager;
		private readonly Database _context;
		private RoleManager<IdentityRole> _roleManager;

        public RegistrationHandler(IJwtGenerator jwtGenerator, UserManager<User> userManager, Database context, RoleManager<IdentityRole> roleManager)
        {
            _jwtGenerator = jwtGenerator ?? throw new ArgumentNullException(nameof(jwtGenerator));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        public async Task<AuthAnswer> Handle(RegistrationCommand request, CancellationToken cancellationToken)
		{
			var errors = new List<RestError>();

			if (await _context.Users.Where(x => x.Email == request.Email).AnyAsync())
				errors.Add(_emailExist);

			if (await _context.Users.Where(x => x.UserName == request.UserName).AnyAsync())
				errors.Add(_userNameExist);

			if (errors.Count != 0)
				throw new RestException(HttpStatusCode.BadRequest, errors);

			var user = new User
			{
				Age = request.Age,
				Email = request.Email,
				UserName = request.UserName
			};

			var result = await _userManager.CreateAsync(user, request.Password);

			
			if (result.Succeeded)
			{
				await _userManager.AddToRoleAsync(user, _defaultUserRole);
				return new AuthAnswer
				{
					Id = user.Id,
					Token = _jwtGenerator.CreateToken(user,_defaultUserRole)
				};
			}
			else
			{ 
				errors = result.Errors.Select(error => new RestError {Reason = error.Code, Message = error.Description}).ToList();
				throw new RestException(HttpStatusCode.BadRequest, errors);
			}

		}
	}
}
