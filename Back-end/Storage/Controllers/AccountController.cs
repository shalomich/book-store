using Auth.Login;
using Auth.Models;
using Auth.Registration;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Auth.Login.LoginHandler;
using static Auth.Registration.RegistrationHandler;

namespace Auth.Controllers
{
    [Route("account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public IActionResult Hello()
        {
            return Content("hello");
        }

        [HttpGet("auth")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult HelloAuth()
        {
            return Content($"hello {User.Identity.Name}");
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthAnswer>> LoginAsync(LoginQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost("registration")]
        public async Task<ActionResult<AuthAnswer>> RegistrationAsync(RegistrationCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
