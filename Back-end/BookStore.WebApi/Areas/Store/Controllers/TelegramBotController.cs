using BookStore.Application.Commands.TelegramBot.CreateTelegramBotToken;
using BookStore.Application.Commands.TelegramBot.RemoveTelegramBotContact;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
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
    public async Task<TelegramBotTokenDto> TelegramBotRegistration(CancellationToken cancellationToken)
    {
        return await Mediator.Send(new CreateTelegramBotTokenCommand(), cancellationToken);
    }

    [HttpDelete]
    public async Task RemoveTelegramBotContact(CancellationToken cancellationToken)
    {
        await Mediator.Send(new RemoveTelegramBotContactCommand(),cancellationToken);
    }
}

