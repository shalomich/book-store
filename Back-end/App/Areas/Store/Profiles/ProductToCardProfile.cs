
using App.Areas.Common.ViewModels;
using App.Areas.Store.ViewModels;
using App.Areas.Store.ViewModels.Cards;
using BookStore.Domain.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Entities.Products;

namespace App.Areas.Store.Profiles
{
    public class ProductToCardProfile : Profile
    {
        public ProductToCardProfile()
        {
            CreateMap<Product, ProductCard>()
                .ForMember(card => card.TitleImage, mapper =>
                    mapper.MapFrom(product => product.Album.TitleImage))
                .IncludeAllDerived();

            CreateMap<Product, FullProductCard>()
                .ForMember(card => card.NotTitleImages, mapper =>
                    mapper.MapFrom(product => product.Album.NotTitleImages))
                .IncludeAllDerived();
        }
    }
}
