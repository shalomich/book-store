using App.Areas.Storage.ViewModels;
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

            CreateMap<AlbumForm, Album>()
                .ReverseMap();

            CreateMap<ImageForm, Image>()
                .ReverseMap();       
        }
    }
}
