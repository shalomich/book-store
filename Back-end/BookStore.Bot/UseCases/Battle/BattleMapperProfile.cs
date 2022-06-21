using AutoMapper;
using BookStore.Application.Queries.Battle.GetBattleInfo;
using BookStore.Application.Queries.UserProfile.GetUserProfile;
using BookStore.Domain.Enums;
using BookStore.Bot.UseCases.Battle.CastVote;
using BookStore.Bot.UseCases.Battle.ViewBattle;
using Microsoft.Extensions.Configuration;
using BookStore.Bot.Providers;

namespace BookStore.Bot.UseCases.Battle;
internal class BattleMapperProfile : Profile
{
    public BattleMapperProfile(IConfiguration configuration)
    {
        CreateMap<BattleInfoDto, BattleInfoViewModel>()
            .ForMember(view => view.State, mapper => mapper.MapFrom(
                dto => Enum.Parse<BattleState>(dto.State)))
            .ForMember(view => view.CurrentDiscount, mapper => mapper.MapFrom(
                dto => dto.DiscountPercentage.HasValue
                    ? dto.DiscountPercentage
                    : dto.InitialDiscount))
            .ForMember(view => view.LeftTime, mapper => mapper.MapFrom(
                dto => dto.EndDate - DateTimeOffset.UtcNow))
            .ForMember(view => view.LeaderBattleBookName, mapper => mapper.MapFrom(
                dto => dto.FirstBattleBook.TotalVotingPointCount > dto.SecondBattleBook.TotalVotingPointCount
                    ? dto.FirstBattleBook.Name
                    : (dto.FirstBattleBook.TotalVotingPointCount < dto.SecondBattleBook.TotalVotingPointCount
                        ? dto.SecondBattleBook.Name
                        : null)));

        CreateMap<BattleBookPreviewDto, BattleBookViewModel>()
            .ForMember(view => view.FileUrl, mapper => mapper.MapFrom(
                dto => dto.TitleImage.FileUrl))
            .ForMember(view => view.StoreUrl, mapper => mapper.MapFrom(
                dto =>  StoreUrlBuilder.BuildBookСardUrl(dto.BookId, configuration)));

        CreateMap<UserProfileDto, UserBattleInfoViewModel>();
    }
}

