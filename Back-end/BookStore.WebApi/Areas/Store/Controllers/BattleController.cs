using BookStore.Application.Commands.Battle;
using BookStore.Application.Queries.Battle.GetBattleInfo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.WebApi.Areas.Store.Controllers;

[ApiController]
[Area("store")]
[Route("[area]/[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class BattleController : ControllerBase
{
    private IMediator Mediator { get; }

    public BattleController(IMediator mediator)
    {
        Mediator = mediator;
    }

    [HttpGet]
    public async Task<BattleInfoDto> GetBattleInfo(CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetBattleInfoQuery(), cancellationToken);
    }

    [HttpPost]
    public async Task CastVote([FromBody][Required] int battleBookId, CancellationToken cancellationToken)
    {
        await Mediator.Send(new CastVoteCommand(battleBookId), cancellationToken);
    }
}

