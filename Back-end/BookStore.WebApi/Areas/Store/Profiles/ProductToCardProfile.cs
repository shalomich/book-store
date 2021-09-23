
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Entities.Products;
using BookStore.WebApi.Areas.Store.ViewModels.Cards;

namespace BookStore.WebApi.Areas.Store.Profiles
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
