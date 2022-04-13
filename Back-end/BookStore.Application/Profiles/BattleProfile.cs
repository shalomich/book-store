
using AutoMapper;
using BookStore.Application.Queries.Battle.GetBattleInfo;
using BookStore.Domain.Entities.Battles;
using System.Linq;

namespace BookStore.Application.Profiles;

internal class BattleProfile : Profile
{
    public BattleProfile()
    {
        CreateMap<BattleBook, BattleBookPreviewDto>()
            .ForMember(dto => dto.BattleBookId, mapper => mapper.MapFrom(
                battleBook => battleBook.Id))
            .ForMember(dto => dto.Name, mapper => mapper.MapFrom(
                battleBook => battleBook.Book.Name))
            .ForMember(dto => dto.AuthorName, mapper => mapper.MapFrom(
                battleBook => battleBook.Book.Author.Name))
            .ForMember(dto => dto.TotalVotingPointCount, mapper => mapper.MapFrom(
                battleBook => battleBook.Votes
                    .Sum(vote => vote.VotingPointCount)))
            .ForMember(dto => dto.TitleImage, mapper => mapper.MapFrom(
                battleBook => battleBook.Book.Album.Images
                  .Single(image => image.Name == battleBook.Book.Album.TitleImageName)));

        CreateMap<Battle, BattleInfoDto>()
            .ForMember(dto => dto.FirstBattleBook, mapper => mapper.MapFrom(
                battle => battle.BattleBooks
                .OrderBy(battleBook => battleBook.Id)
                .First()))
            .ForMember(dto => dto.SecondBattleBook, mapper => mapper.MapFrom(
                battle => battle.BattleBooks
                .OrderByDescending(battleBook => battleBook.Id)
                .First()));
    }
}

