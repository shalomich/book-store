﻿using App.Areas.Store.ViewModels;
using BookStore.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Store.Profiles
{
    public class EntityToOptionProfile : Profile
    {
        public EntityToOptionProfile()
        {
            CreateMap<RelatedEntity, Option>()
                .ForMember(option => option.Text, mapper
                    => mapper.MapFrom(entity => entity.Name))
                .ForMember(option => option.Value, mapper
                    => mapper.MapFrom(entity => entity.Id.ToString()));
        }
    }
}
