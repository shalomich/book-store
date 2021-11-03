using AutoMapper;
using BookStore.Application.Dto;
using BookStore.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Profiles
{
    internal class ProductToDtoProfile : Profile
    {
        public ProductToDtoProfile()
        {
            CreateMap<Image, ImageDto>();
            CreateMap<Album, AlbumDto>();
        }
    }
}
