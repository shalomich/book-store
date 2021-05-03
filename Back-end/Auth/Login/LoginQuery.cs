using Auth.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Login
{
	public class LoginQuery : IRequest<AuthAnswer>
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
