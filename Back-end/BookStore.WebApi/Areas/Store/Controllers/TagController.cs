using BookStore.Application.Commands.Tags.UpdateUserTags;
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

    [HttpPut]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task UpdateUserTags(UserTagsDto userTagsDto, CancellationToken cancellationToken)
    {
        await Mediator.Send(new UpdateUserTagsCommand(userTagsDto), cancellationToken);
    }
}

