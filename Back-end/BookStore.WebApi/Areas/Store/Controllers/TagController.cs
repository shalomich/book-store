using BookStore.Application.Commands.Tags.AddTagByUser;
using BookStore.Application.Commands.Tags.Common;
using BookStore.Application.Commands.Tags.RemoveTagByUser;
using BookStore.Application.Queries.Tags.GetTagGroups;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.WebApi.Areas.Store.Controllers;

[ApiController]
[Area("store")]
[Route("[area]/tag")]
public class TagController : ControllerBase
{
    private IMediator Mediator { get; }

    public TagController(IMediator mediator)
    {
        Mediator = mediator;
    }

    [HttpGet("group")]
    public async Task<IEnumerable<TagGroupDto>> GetTagGroups(CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetTagGroupsQuery(), cancellationToken);
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task AddTagByUser(UserTagDto tagDto, CancellationToken cancellationToken)
    {
        await Mediator.Send(new AddTagByUserCommand(tagDto), cancellationToken);
    }

    [HttpDelete]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task RemoveTagByUser(UserTagDto tagDto, CancellationToken cancellationToken)
    {
        await Mediator.Send(new RemoveTagByUserCommand(tagDto), cancellationToken);
    }
}

