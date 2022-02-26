using AutoMapper;
using BookStore.Application.Dto;
using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Profiles
{
    internal class OrderProfile : Profile
    {
        public OrderProfile()
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

            CreateMap<BasketProduct, OrderProduct>()
                .ForMember(orderProduct => orderProduct.Name, mapper =>
                    mapper.MapFrom(basketProduct => basketProduct.Product.Name))
                .ForMember(orderProduct => orderProduct.Cost, mapper =>
                    mapper.MapFrom(basketProduct => basketProduct.Product.Cost))
                .ForMember(orderProduct => orderProduct.Id, mapper =>
                    mapper.Ignore());

            ;
        }
    }
}
