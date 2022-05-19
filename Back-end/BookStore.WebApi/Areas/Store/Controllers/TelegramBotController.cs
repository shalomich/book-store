using BookStore.Application.Commands.TelegramBot.CreateTelegramBotToken;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.WebApi.Areas.Store.Controllers;

[ApiController]
[Area("store")]
[Route("[area]/telegram-bot")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class TelegramBotController : ControllerBase
{
    private IMediator Mediator { get; }

    public TelegramBotController(IMediator mediator)
    {
        Mediator = mediator;
    }

    [HttpPost("token")]
    public async Task<TelegramBotTokenDto> TelegramBotRegistration()
    {
        return await Mediator.Send(new CreateTelegramBotTokenCommand());
    }
}

