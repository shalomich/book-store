using AutoMapper;
using BookStore.Application.Commands.Selection.Common;

namespace BookStore.TelegramBot.UseCases.ViewSelection;
public class SelectionMapperProfile : Profile
{
    public SelectionMapperProfile()
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
                dto => $"https://comicstore-de688.web.app/book-store/catalog/book/{dto.Id}"));
    }
}