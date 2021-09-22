using App.Areas.Common.ViewModels;
using App.Areas.Dashboard.ViewModels;
using App.Areas.Dashboard.ViewModels.Forms;
using BookStore.Domain.Entities;
using BookStore.Domain.Entities.Books;
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
