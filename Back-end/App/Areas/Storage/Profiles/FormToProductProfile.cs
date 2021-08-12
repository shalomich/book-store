using App.Areas.Storage.ViewModels;
using App.Areas.Storage.ViewModels.Identities;
using App.Entities;
using App.Entities.Products;
using App.Products.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Storage.Profiles
{
    public class FormToProductProfile : Profile
    {
        public FormToProductProfile()
        {
            CreateMap<ProductForm, Product>()
                .ReverseMap()
                .IncludeAllDerived();

            CreateMap<Product, ProductIdentity>()
                .ForMember(identity => identity.TitleImage, mapper =>
                    mapper.MapFrom(product => product.Album.TitleImage))
                .IncludeAllDerived();

            CreateMap<AlbumDto, Album>()
                .ReverseMap();

            CreateMap<ImageDto, Image>()
                .ReverseMap();       
        }
    }
}
