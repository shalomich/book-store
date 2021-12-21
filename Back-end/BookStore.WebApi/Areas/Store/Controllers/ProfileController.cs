using AutoMapper;
using BookStore.Application.Commands;
using BookStore.Application.Dto;
using BookStore.Application.Queries;
using BookStore.Application.Services.DbQueryBuilders;
using BookStore.Domain.Entities;
using BookStore.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<ActionResult<UserDto>> Update(UserDto userDto)
        {
            var user = await GetCurrentUser();

            user = Mapper.Map(userDto, user);

            await Mediator.Send(new UpdateCommand(User.GetUserId(), user));

            return NoContent();
        }
    }
}
