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
    public class FormToPublicationProfile : Profile
    {
        public FormToPublicationProfile()
        {
            CreateMap<Publication, ProductIdentity>();
            CreateMap<PublicationForm, Publication>()
                .ForMember(publication => publication.GenresPublications,
                    mapper => mapper.MapFrom(form => form.GenreIds
                    .Select(id => new GenrePublication { GenreId = id })))
            .ReverseMap()
                .ForMember(form => form.GenreIds,
                    mapper => mapper.MapFrom(publication => publication.GenresPublications
                        .Select(genre => genre.GenreId)));

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

            CreateMap<PublisherForm, Publisher>()
              .ReverseMap();

            CreateMap<GenreForm, Genre>()
              .ReverseMap();

            
            CreateMap<CoverArtForm, CoverArt>()
              .ReverseMap();

            CreateMap<PublicationTypeForm, PublicationType>()
              .ReverseMap();

            CreateMap<AgeLimitForm, AgeLimit>()
              .ReverseMap();   
        }
    }
}
