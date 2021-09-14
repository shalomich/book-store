using App.Areas.Common.ViewModels;
using App.Areas.Dashboard.ViewModels;
using App.Areas.Dashboard.ViewModels.Forms;
using App.Entities;
using App.Entities.Books;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Dashboard.Profiles
{
    public class FormToRelatedEntityProfile : Profile
    {
        public FormToRelatedEntityProfile()
        {
            CreateMap<RelatedEntity, RelatedEntityForm>()
                .ReverseMap();
        }
    }
}
