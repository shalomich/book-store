
using BookStore.Application.Commands.Account;
using BookStore.Application.ViewModels.Account;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Store.Controllers
{
    [ApiController]
    [Area("store")]
    [Route("[area]/[controller]")]
    public class AccountController : ControllerBase
    {
        private IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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
