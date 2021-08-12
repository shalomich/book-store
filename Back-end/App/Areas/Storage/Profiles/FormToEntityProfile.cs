using App.Areas.Storage.ViewModels;
using App.Areas.Storage.ViewModels.Identities;
using App.Entities;
using App.Entities.Publications;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Storage.Profiles
{
    public class FormToEntityProfile : Profile
    {
        public FormToEntityProfile()
        {
            CreateMap<EntityForm, Entity>()
                .ReverseMap()
                .IncludeAllDerived();

            CreateMap<Entity, EntityIdentity>()
                .IncludeAllDerived();
        }
    }
}
