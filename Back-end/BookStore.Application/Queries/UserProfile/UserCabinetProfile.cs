using AutoMapper;
using BookStore.Application.Commands.UserProfile.UpdateUserProfile;
using BookStore.Application.Queries.UserProfile.GetUserProfile;
using BookStore.Domain.Entities;
using System.Linq;

namespace BookStore.Application.Profiles;
internal class UserCabinetProfile : Profile
{
    public UserCabinetProfile()
    {
        CreateMap<User, UserProfileDto>()
            .ForMember(dto => dto.IsTelegramBotLinked, mapper => mapper.MapFrom
                (user => user.TelegramBotContact.IsAuthenticated))
            .ForMember(dto => dto.BasketBookIds, mapper => mapper.MapFrom
                (user => user.BasketProducts
                    .Select(basketProduct => basketProduct.ProductId)))
            .ForMember(dto => dto.MarkBookIds, mapper => mapper.MapFrom
                (user => user.Marks
                    .Select(mark => mark.ProductId)))
            .ForMember(dto => dto.CurrentVotedBattleBookId, mapper => mapper.MapFrom
                (user => user.Votes
                    .FirstOrDefault()
                    .BattleBookId))
            .ForMember(dto => dto.SpentCurrentVotingPointCount, mapper => mapper.MapFrom
                (user => user.Votes
                    .FirstOrDefault()
                    .VotingPointCount));

        CreateMap<UserProfileForm, User>();
    }
}

