using AutoMapper;
using BookStore.Application.Commands;
using BookStore.Application.Commands.UserProfile;
using BookStore.Application.Dto;
using BookStore.Application.Queries;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Application.ViewModels.Profile;
using BookStore.Domain.Entities;
using BookStore.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace BookStore.WebApi.Areas.Store.Controllers
{
    [Route("[area]/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ProfileController : StoreController
    {
        private IMediator Mediator { get; }
        private IMapper Mapper { get; }
        private DbEntityQueryBuilder<User> UserQueryBuilder { get; }

        public ProfileController(IMediator mediator, IMapper mapper, DbEntityQueryBuilder<User> userQueryBuilder)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            UserQueryBuilder = userQueryBuilder ?? throw new ArgumentNullException(nameof(userQueryBuilder));
        }

        private async Task<User> GetCurrentUser()
        {
            return (User) await Mediator.Send(new GetByIdQuery(User.GetUserId(), UserQueryBuilder));
        }

        [HttpGet]
        public async Task<UserDto> Get()
        {
            var user = await GetCurrentUser();

            return Mapper.Map<UserDto>(user);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserProfileForm profileForm)
        {
            var user = await GetCurrentUser();

            user = Mapper.Map(profileForm, user);

            await Mediator.Send(new UpdateCommand(User.GetUserId(), user));

            return NoContent();
        }

        [HttpPost("mark")]
        public async Task<NoContentResult> CreateMark([FromBody] int bookId)
        {
            await Mediator.Send(new CreateMarkCommand(bookId));

            return NoContent();
        }

        [HttpDelete("mark")]
        public async Task<NoContentResult> RemoveMark([FromBody] int bookId)
        {
            await Mediator.Send(new RemoveMarkCommand(bookId));

            return NoContent();
        }

        [HttpGet("tag")]
        public async Task<IEnumerable<RelatedEntityDto>> GetTags()
        {
            return await Mediator.Send(new GetUserTagsQuery(User.GetUserId()));
        }

        [HttpPut("tag")]
        public async Task<NoContentResult> UpdateTags([FromBody][Required] int[] tagIds)
        {
            await Mediator.Send(new UpdateUserTagsCommand(User.GetUserId(), tagIds));

            return NoContent();
        }
    }
}
