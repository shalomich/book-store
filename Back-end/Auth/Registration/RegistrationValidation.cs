using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Auth.Registration
{
	public class RegistrationValidation : AbstractValidator<RegistrationCommand>
	{
		public RegistrationValidation()
		{
			RuleFor(x => x.UserName).NotEmpty();
			RuleFor(x => x.Email).NotEmpty().EmailAddress();
			RuleFor(x => x.Password).NotEmpty();
		}
	}
}
