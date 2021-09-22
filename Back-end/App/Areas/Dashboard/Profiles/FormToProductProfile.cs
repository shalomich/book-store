using App.Areas.Common.ViewModels;
using App.Areas.Dashboard.ViewModels;
using BookStore.Domain.Entities;
using BookStore.Domain.Entities.Products;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Dashboard.Profiles
{
    public class FormToProductProfile : Profile
    {
        public FormToProductProfile()
        {
            CreateMap<ProductForm, Product>()
                .ReverseMap()
                .IncludeAllDerived();

            CreateMap<AlbumDto, Album>()
                .ReverseMap();

            CreateMap<ImageDto, Image>()
                .ReverseMap();       
        }
    }
}
