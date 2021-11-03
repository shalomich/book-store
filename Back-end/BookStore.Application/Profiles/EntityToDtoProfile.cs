using AutoMapper;
using BookStore.Application.Dto;
using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Profiles
{
    internal class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<RelatedEntity, RelatedEntityDto>();
        }
    }
}
