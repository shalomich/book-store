using AutoMapper;
using BookStore.Application.Commands.Basket.GetBasketProducts;
using BookStore.Domain.Entities;

namespace BookStore.Application.Commands.Basket;
internal class BasketProfile : Profile
{
    public BasketProfile()
    {
        CreateMap<BasketProduct, BasketProductDto>()
            .ForMember(dto => dto.Name, mapper
                => mapper.MapFrom(basketProduct => basketProduct.Product.Name))
            .ForMember(dto => dto.Cost, mapper
                => mapper.MapFrom(basketProduct => basketProduct.Product.Cost))
            .ForMember(dto => dto.Quantity, mapper
                => mapper.MapFrom(basketProduct => basketProduct.Quantity))
            .ForMember(dto => dto.TitleImage, mapper
                => mapper.MapFrom(basketProduct => basketProduct.Product.Album.TitleImage))
            .IncludeAllDerived();
    }
}

