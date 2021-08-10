﻿using App.Areas.Storage.ViewModels;
using App.Entities;
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

        }
    }
}