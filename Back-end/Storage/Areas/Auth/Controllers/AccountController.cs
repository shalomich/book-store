using App.Areas.Auth.ViewModels;
using Auth.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static App.Areas.Auth.RequestHandlers.LoginHandler;
using static App.Areas.Auth.RequestHandlers.RegistrationHandler;

namespace App.Areas.Auth.Controllers
{
    [ApiController]
    [Area("auth")]
    [Route("[area]/[controller]")]
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
            return Content($"{User.Identity.IsAuthenticated}");
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthorizedData>> LoginAsync(AuthForm authForm)
        {
            return await _mediator.Send(new LoginCommand(authForm));
        }

        [HttpPost("registration")]
        public async Task<ActionResult<AuthorizedData>> RegistrationAsync(AuthForm authForm)
        {
            return await _mediator.Send(new RegistrationCommand(authForm));
        }
    }
}
