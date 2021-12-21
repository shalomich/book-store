using AutoMapper;
using BookStore.Application.Dto;
using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Profiles
{
    internal class UserToDtoProfile : Profile
    {
        public UserToDtoProfile()
        {
            CreateMap<User, UserDto>()
                .ReverseMap();
        }
    }
}
