
using BookStore.Domain.Entities.Products;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Areas.Dashboard.ViewModels.Forms;
using BookStore.Application.Dto;

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
