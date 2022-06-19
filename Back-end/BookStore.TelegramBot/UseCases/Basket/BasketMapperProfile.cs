
using AutoMapper;
using BookStore.Application.Commands.Basket.GetBasketProducts;
using BookStore.TelegramBot.UseCases.Basket.ViewBasketProducts;

namespace BookStore.TelegramBot.UseCases.Basket;
internal class BasketMapperProfile : Profile
{
    public BasketMapperProfile()
    {
        CreateMap<BasketProductDto, BasketProductViewModel>()
              .ForMember(view => view.FileUrl, mapper => mapper.MapFrom(
                  dto => dto.TitleImage.FileUrl))
              .ForMember(view => view.StoreUrl, mapper => mapper.MapFrom(
                  dto => $"https://comicstore-de688.web.app/book-store/catalog/book/{dto.Id}"));
    }
}

