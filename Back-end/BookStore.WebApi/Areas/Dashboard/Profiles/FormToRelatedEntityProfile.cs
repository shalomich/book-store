
using BookStore.Domain.Entities;
using BookStore.Domain.Entities.Books;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.WebApi.Areas.Dashboard.ViewModels.Forms;

namespace BookStore.WebApi.Areas.Dashboard.Profiles
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
