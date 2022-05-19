using AutoMapper;
using BookStore.Application.Commands.UserProfile.UpdateUserProfile;
using BookStore.Application.Queries.UserProfile.GetUserProfile;
using BookStore.Domain.Entities;

namespace BookStore.Application.Profiles;
internal class UserCabinetProfile : Profile
{
    public UserCabinetProfile()
    {
        CreateMap<User, UserProfileDto>()
            .ForMember(dto => dto.IsTelegramBotLinked, mapper => mapper.MapFrom
                (user => user.TelegramBotContact.IsAuthenticated));

        CreateMap<UserProfileForm, User>();
    }
}

