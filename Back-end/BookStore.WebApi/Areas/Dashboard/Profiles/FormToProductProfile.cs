
using BookStore.Domain.Entities.Products;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Application.Dto;
using BookStore.WebApi.Areas.Dashboard.ViewModels.Forms;

namespace BookStore.WebApi.Areas.Dashboard.Profiles
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
