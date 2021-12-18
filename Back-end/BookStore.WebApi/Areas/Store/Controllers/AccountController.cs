
using BookStore.Application.Commands.Account;
using BookStore.Application.Dto;
using BookStore.Application.Queries;
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

        [HttpPost("logout")]
        public Task<Unit> Logout(TokensDto tokens)
        {
            return _mediator.Send(new LogoutCommand(tokens));
        }

        [HttpPost("refresh")]
        public Task<TokensDto> RefreshToken(TokensDto tokens)
        {
            return _mediator.Send(new RefreshTokenCommand(tokens));
        }

        [HttpGet("email-existence/{email}")]
        public async Task<bool> CheckEmailExistence(string email)
        {
            try
            {
                await _mediator.Send(new FindUserByEmailQuery(email));

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
