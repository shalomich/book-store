using AutoMapper;
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
            CreateMap<BasketProduct, OrderProduct>()
                .ForMember(orderProduct => orderProduct.Name, mapper =>
                    mapper.MapFrom(basketProduct => basketProduct.Product.Name))
                .ForMember(orderProduct => orderProduct.Cost, mapper =>
                    mapper.MapFrom(basketProduct => basketProduct.Product.Cost))
                .ForMember(orderProduct => orderProduct.Id, mapper =>
                    mapper.Ignore());
        }
    }
}
