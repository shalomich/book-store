using BookStore.Application.Commands.UserProfile;
using BookStore.Application.Commands.UserProfile.CreateMark;
using BookStore.Application.Commands.UserProfile.RemoveMark;
using BookStore.Application.Commands.UserProfile.UpdateUserProfile;
using BookStore.Application.Commands.UserProfile.UpdateUserTags;
using BookStore.Application.Dto;
using BookStore.Application.Queries.UserProfile.GetUserProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.WebApi.Areas.Store.Controllers;

[Route("[area]/[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class ProfileController : StoreController
{
    private IMediator Mediator { get; }
 
    public ProfileController(IMediator mediator)
    {
        Mediator = mediator;
    }

    [HttpGet]
    public async Task<UserProfileDto> Get(CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetUserProfileQuery(), cancellationToken);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UserProfileForm profileForm, CancellationToken cancellationToken)
    {
        await Mediator.Send(new UpdateUserProfileCommand(profileForm), cancellationToken);

        return NoContent();
    }

    [HttpPost("mark")]
    public async Task<NoContentResult> CreateMark([FromBody] int bookId, CancellationToken cancellationToken)
    {
        await Mediator.Send(new CreateMarkCommand(bookId), cancellationToken);

        return NoContent();
    }

    [HttpDelete("mark")]
    public async Task<NoContentResult> RemoveMark([FromBody] int bookId, CancellationToken cancellationToken)
    {
        await Mediator.Send(new RemoveMarkCommand(bookId), cancellationToken);

        return NoContent();
    }

    [HttpGet("tag")]
    public async Task<IEnumerable<RelatedEntityDto>> GetTags(CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetUserTagsQuery(), cancellationToken);
    }

    [HttpPut("tag")]
    public async Task<NoContentResult> UpdateTags([FromBody][Required] int[] tagIds, CancellationToken cancellationToken)
    {
        await Mediator.Send(new UpdateUserTagsCommand(tagIds), cancellationToken);

        return NoContent();
    }
}

