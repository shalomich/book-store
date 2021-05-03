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

namespace Auth.Controllers
{
    [AllowAnonymous]
    [Route("auth")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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
