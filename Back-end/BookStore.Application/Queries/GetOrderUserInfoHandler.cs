using AutoMapper;
using BookStore.Application.Dto;
using BookStore.Application.Services;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Queries
{
    public record GetOrderUserInfoQuery() : IRequest<OrderUserInfo>;
    internal class GetOrderUserInfoHandler : IRequestHandler<GetOrderUserInfoQuery, OrderUserInfo>
    {
        private ApplicationContext Context { get; }
        private IMapper Mapper { get; }
        private LoggedUserAccessor LoggedUserAccessor { get; }

        public GetOrderUserInfoHandler(ApplicationContext context, IMapper mapper, LoggedUserAccessor loggedUserAccessor)
        {
            Context = context;
            Mapper = mapper;
            LoggedUserAccessor = loggedUserAccessor;
        }

        public async Task<OrderUserInfo> Handle(GetOrderUserInfoQuery request, CancellationToken cancellationToken)
        {
            var userId = LoggedUserAccessor.GetCurrentUserId();

            var userById = await Context.Users
                .SingleAsync(user => user.Id == userId);

            var userInfo = Mapper.Map<OrderUserInfo>(userById);

            if (userById.UserName != userById.Email)
            {
                var firstNameAndLastName = userById.UserName.Split(" ");
                userInfo.FirstName = firstNameAndLastName[0];
                userInfo.LastName = firstNameAndLastName[1];
            }

            return userInfo;
        }
    }
}
