using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Login
{
	public class LoginQueryValidation : AbstractValidator<LoginQuery>
	{
		public LoginQueryValidation()
		{
			RuleFor(x => x.Email).EmailAddress().NotEmpty();
			RuleFor(x => x.Password).NotEmpty();
		}
	}
}
