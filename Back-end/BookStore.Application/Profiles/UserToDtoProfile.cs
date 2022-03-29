using AutoMapper;
using BookStore.Application.Dto;
using BookStore.Application.ViewModels.Profile;
using BookStore.Domain.Entities;

namespace BookStore.Application.Profiles
{
    internal class UserToDtoProfile : Profile
    {
        public UserToDtoProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<UserProfileForm, User>();
        }
    }
}
