
using BookStore.Application.Commands.Account;
using BookStore.Application.Dto;
using BookStore.Application.ViewModels.Account;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApi.Areas.Store.Controllers
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
        public Task<TokensDto> Login(AuthForm authForm)
        {
            return _mediator.Send(new LoginCommand(authForm));
        }

        [HttpPost("registration")]
        public Task<TokensDto> Registration(AuthForm authForm)
        {
            return _mediator.Send(new RegistrationCommand(authForm));
        }

        [HttpPost("refresh")]
        public Task<TokensDto> RefreshToken(TokensDto tokensDto)
        {
            return _mediator.Send(new RefreshTokenCommand(tokensDto));
        }
    }
}
