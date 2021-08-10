using App.Areas.Storage.ViewModels;
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

            CreateMap<GenreForm, Genre>()
              .ReverseMap();

            CreateMap<PublisherForm, Publisher>()
              .ReverseMap();

            CreateMap<CoverArtForm, CoverArt>()
              .ReverseMap();

            CreateMap<PublicationTypeForm, PublicationType>()
              .ReverseMap();

            CreateMap<AgeLimitForm, AgeLimit>()
              .ReverseMap();

            CreateMap<AuthorForm, Author>()
                .ForMember(author => author.Name,
                    mapper => mapper.MapFrom(form => form.Name))
            .ReverseMap()
                .ForMember(form => form.Surname,
                    mapper => mapper.MapFrom(author => author.Surname))
                .ForMember(form => form.FirstName,
                    mapper => mapper.MapFrom(author => author.FirstName))
                .ForMember(form => form.Patronymic,
                    mapper => mapper.MapFrom(author => author.Patronymic));
        }
    }
}
