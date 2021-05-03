using Auth.Exceptions;
using Auth.Models;
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
		private static readonly RestError _emailExist = new RestError { Reason = "EmailExist", Message = "Current email belongs to other" };
		private static readonly RestError _userNameExist = new RestError { Reason = "UserNameExist", Message = "Current userName belongs to other" };


		private readonly UserManager<User> _userManager;
		private readonly Database _context;

		public RegistrationHandler(Database context, UserManager<User> userManager)
		{
			_context = context;
			_userManager = userManager;
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
				return new AuthAnswer
				{
					Id = user.Id,
					Token = "test"
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
