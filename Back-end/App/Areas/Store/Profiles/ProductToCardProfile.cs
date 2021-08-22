
using App.Areas.Store.ViewModels.Cards;
using App.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
