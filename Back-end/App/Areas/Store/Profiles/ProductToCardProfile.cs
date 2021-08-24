﻿
using App.Areas.Common.ViewModels;
using App.Areas.Store.ViewModels;
using App.Areas.Store.ViewModels.Cards;
using App.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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

            /*CreateMap<FormEntitiesByQuery, ProductCardsByQuery>()
                .ForMember(cardsByQuery => cardsByQuery.Cards, mapper
                    => mapper.MapFrom(formEntitiesByQuery => formEntitiesByQuery.FormEntities
                        .ProjectTo(DefaultMemberConfig));*/
        }
    }
}
