using AutoMapper;
using BookStore.Application.Commands.Selection.Common;
using BookStore.TelegramBot.UseCases.Common;
using Microsoft.Extensions.Configuration;

namespace BookStore.TelegramBot.UseCases.ViewSelection;
public class SelectionMapperProfile : Profile
{
    public SelectionMapperProfile(IConfiguration configuration)
    {
        CreateMap<PreviewSetDto, PreviewSetViewModel>();

        CreateMap<PreviewDto, PreviewViewModel>()
            .ForMember(view => view.FileUrl, mapper => mapper.MapFrom(
                dto => dto.TitleImage.FileUrl))
            .ForMember(view => view.DiscountCost, mapper => mapper.MapFrom(
                dto => dto.DiscountPercentage.HasValue
                    ? dto.DiscountPercentage / 100.0 * dto.Cost
                    : null))
            .ForMember(view => view.StoreUrl, mapper => mapper.MapFrom(
                dto => StoreUrlBuilder.BuildBookСardUrl(dto.Id, configuration)));
    }
}