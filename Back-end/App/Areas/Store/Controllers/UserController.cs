using App.Entities;
using App.Services.QueryBuilders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static App.Areas.Common.RequestHandlers.GetByIdHandler;

namespace App.Areas.Store.Controllers
{
    
    [Route("[area]/user/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UserController : StoreController
    {
        protected IMediator Mediator { get; }
        protected DbEntityQueryBuilder<User> UserQueryBuilder { get; }

        public UserController(IMediator mediator, DbEntityQueryBuilder<User> userQueryBuilder)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            UserQueryBuilder = userQueryBuilder ?? throw new ArgumentNullException(nameof(userQueryBuilder));
        }

        protected async Task<User> GetAuthorizedUser(DbEntityQueryBuilder<User> userQueryBuilder) { 

            int userId = int.Parse(User.FindFirst("id").Value);

            var user = (User) await Mediator.Send(new GetByIdQuery(userId, userQueryBuilder));

            return user;
        }
    }
}
