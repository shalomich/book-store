
using BookStore.Application.Commands.Account;
using BookStore.Application.Commands.Account.ResetPassword;
using BookStore.Application.Commands.Account.SendResetPasswordMessage;
using BookStore.Application.Commands.Account.SendTwoFactorAuthenticateMessage;
using BookStore.Application.Commands.Account.TwoFactorAuthenticate;
using BookStore.Application.Commands.TelegramBot.CreateTelegramBotToken;
using BookStore.Application.Dto;
using BookStore.Application.Queries;
using BookStore.Application.ViewModels.Account;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        public async Task<TokensDto> Login(LoginForm authForm)
        {
            var user = await _mediator.Send(new FindUserByEmailQuery(authForm.Email));

            return await _mediator.Send(new LoginCommand(user, authForm.Password));
        }

        [HttpPost("registration")]
        public Task<TokensDto> Registration(RegistrationForm authForm)
        {
            return _mediator.Send(new RegistrationCommand(authForm.Email, authForm.FirstName, authForm.Password));
        }

        [HttpPost("logout")]
        public async Task<Unit> Logout(TokensDto tokens)
        {
            return await _mediator.Send(new LogoutCommand(tokens.RefreshToken));
        }

        [HttpPost("refresh")]
        public async Task<TokensDto> RefreshToken(TokensDto tokens)
        {
            return await _mediator.Send(new RefreshTokenCommand(tokens));
        }

        [HttpPost("forgot-password")]
        public async Task ForgotPassword(ForgotPasswordDto forgotPasswordDto, CancellationToken cancellationToken)
        {
            await _mediator.Send(new SendResetPasswordMessageCommand(forgotPasswordDto), cancellationToken);
        }

        [HttpPost("reset-password")]
        public async Task ResetPassword(ResetPasswordDto resetPasswordDto, CancellationToken cancellationToken)
        {
            await _mediator.Send(new ResetPasswordCommand(resetPasswordDto), cancellationToken);
        }

        [HttpPost("two-factor-auth-code")]
        public async Task SendTwoFactorAuthenticateMessage(CancellationToken cancellationToken)
        {
            await _mediator.Send(new SendTwoFactorAuthenticateMessageCommand(), cancellationToken);
        }

        [HttpPost("two-factor-auth")]
        public async Task TwoFactorAuthenticate(TwoFactorAuthenticateDto twoFactorAuthenticateDto, CancellationToken cancellationToken)
        {
            await _mediator.Send(new TwoFactorAuthenticateCommand(twoFactorAuthenticateDto), cancellationToken);
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
