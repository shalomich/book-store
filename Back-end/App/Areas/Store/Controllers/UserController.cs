using App.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static App.Areas.Common.RequestHandlers.GetEntityByIdHandler;

namespace App.Areas.Store.Controllers
{
    
    [Route("[area]/user/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UserController : StoreController
    {
        protected IMediator Mediator { get; }

        public UserController(IMediator mediator)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        protected async Task<User> GetAuthorizedUser() { 

            int userId = int.Parse(User.FindFirst("id").Value);

            var user = (User) await Mediator.Send(new GetByIdQuery(userId, typeof(User)));

            return user;
        }
    }
}
