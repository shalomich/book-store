
using AutoMapper;
using BookStore.Application.Commands.Basket.GetBasketProducts;
using BookStore.Bot.Providers;
using BookStore.Bot.UseCases.Basket.ViewBasketProducts;
using Microsoft.Extensions.Configuration;

namespace BookStore.Bot.UseCases.Basket;
internal class BasketMapperProfile : Profile
{
    public BasketMapperProfile(IConfiguration configuration)
    {
        var maxQuantityForChoosenProduct = configuration
            .GetSection("BackEnd:MaxQuantityForChoosenProduct")
            .Get<int>();

        CreateMap<BasketProductDto, BasketProductViewModel>()
              .ForMember(view => view.MaxQuantity, mapper => mapper.MapFrom(
                  dto => dto.ProductQuantity > maxQuantityForChoosenProduct
                    ? maxQuantityForChoosenProduct
                    : dto.ProductQuantity))
              .ForMember(view => view.FileUrl, mapper => mapper.MapFrom(
                  dto => dto.TitleImage.FileUrl))
              .ForMember(view => view.StoreUrl, mapper => mapper.MapFrom(
                  dto => StoreUrlBuilder.BuildBookСardUrl(dto.ProductId, configuration)));
    }
}

