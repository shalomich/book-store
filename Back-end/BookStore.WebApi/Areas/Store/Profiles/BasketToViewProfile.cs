﻿using AutoMapper;
using BookStore.Domain.Entities;
using BookStore.WebApi.Areas.Store.ViewModels.Basket;

namespace BookStore.WebApi.Areas.Store.Profiles;
public class BasketToViewProfile : Profile
{
    public BasketToViewProfile()
    {
        CreateMap<BasketProduct, BasketProductView>()
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