using AutoMapper;
using BookStore.Application.Commands.Selection.Common;

namespace BookStore.TelegramBot.Commands.Selection;
public class SelectionMapperProfile : Profile
{
    public SelectionMapperProfile()
    {
        CreateMap<PreviewDto, TelegramPreviewDto>()
            .ForMember(dto => dto.FileUrl, mapper => mapper.MapFrom(
                preview => preview.TitleImage.FileUrl))
            .ForMember(dto => dto.FileUrl, mapper => mapper.MapFrom(
                preview => preview.TitleImage.FileUrl))
            .ForMember(dto => dto.DiscountCost, mapper => mapper.MapFrom(
                preview => preview.DiscountPercentage.HasValue
                    ? preview.DiscountPercentage / 100.0 * preview.Cost
                    : null))
            .ForMember(dto => dto.StoreUrl, mapper => mapper.MapFrom(
                preview => $"https://comicstore-de688.web.app/book-store/catalog/book/{preview.Id}"));
    }
}