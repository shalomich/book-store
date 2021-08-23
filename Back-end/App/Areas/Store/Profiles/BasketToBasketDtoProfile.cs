using App.Areas.Store.ViewModels.Basket;
using App.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Store.Profiles
{
    public class BasketToBasketDtoProfile : Profile
    {
        public BasketToBasketDtoProfile()
        {
            CreateMap<BasketProduct, BasketProductDto>()
                .ForMember(dto => dto.Name, mapper
                    => mapper.MapFrom(basketProduct => basketProduct.Product.Name))
                .ForMember(dto => dto.Cost, mapper
                    => mapper.MapFrom(basketProduct => basketProduct.Product.Cost))
                .ForMember(dto => dto.TitleImage, mapper
                    => mapper.MapFrom(basketProduct => basketProduct.Product.Album.TitleImage));

            CreateMap<Basket, BasketDto>();

        }
    }
}
