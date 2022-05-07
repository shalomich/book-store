using BookStore.Application.Commands.Battles;
using BookStore.Application.Commands.Battles.UpdateBattleSettings;
using BookStore.Application.Queries.Battle.GetBattleInfo;
using BookStore.Application.Queries.Battle.GetBattleSettings;
using BookStore.WebApi.Attributes;
using BookStore.WebApi.BackgroundJobs.Battles;
using Hangfire;
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
    [AllowAnonymous]
    [TypeFilter(typeof(OptionalAuthorizeFilter))]
    public async Task<BattleInfoDto> GetBattleInfo(CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetBattleInfoQuery(), cancellationToken);
    }

    [HttpPost]
    public void StartBattle()
    {
        BackgroundJob.Enqueue<StartBattleJob>(job => job.StartBattle(default));
    }

    [HttpPost("vote")]
    public async Task CastVote([FromBody][Required] int battleBookId, CancellationToken cancellationToken)
    {
        await Mediator.Send(new CastVoteCommand(battleBookId), cancellationToken);
    }

    [HttpPut("vote")]
    public async Task SpendVotingPoints([FromBody][Required] int votingPointCount, CancellationToken cancellationToken)
    {
        await Mediator.Send(new SpendVotingPointsCommand(votingPointCount), cancellationToken);
    }

    [HttpGet("settings")]
    public async Task<BattleSettings> GetBattlesSettings(CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetBattlesSettingsQuery(), cancellationToken);
    }

    [HttpPut("settings")]
    public async Task UpdateBattlesSettings(BattleSettings battleSettings, CancellationToken cancellationToken)
    {
        await Mediator.Send(new UpdateBattleSettingsCommand(battleSettings), cancellationToken);
    }
}

