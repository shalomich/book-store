using App.Areas.Common.ViewModels;
using App.Areas.Dashboard.ViewModels;
using App.Areas.Dashboard.ViewModels.Identities;
using App.Entities;
using App.Entities.Publications;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Dashboard.Profiles
{
    public class FormToEntityProfile : Profile
    {
        public FormToEntityProfile()
        {
            CreateMap<EntityForm, FormEntity>()
                .ReverseMap()
                .IncludeAllDerived();

            CreateMap<FormEntity, FormEntityIdentity>()
                .IncludeAllDerived();

            CreateMap<FormEntitiesByQuery, FormEntityIdentitiesByQuery>();
        }
    }
}
