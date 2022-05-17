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
            CreateMap<User, UserProfileDto>()
                .ForMember(dto => dto.IsTelegramBotLinked, mapper => mapper.MapFrom
                    (user => user.TelegramBotContact.IsAuthenticated));

            CreateMap<UserProfileForm, User>();
        }
    }
}
