
using App.Areas.Store.ViewModels.Cards;
using App.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Store.Profiles
{
    public class PublicationToCardProfile : Profile
    {
        public PublicationToCardProfile()
        {
            CreateMap<Publication, ProductCard>();
            CreateMap<Publication, PublicationCard>()
                .ForMember(card => card.PublisherName, mapper =>
                    mapper.MapFrom(publication => publication.Publisher.Name))
                .ForMember(card => card.AuthorName, mapper =>
                    mapper.MapFrom(publication => publication.Author.Name))
                .ForMember(card => card.Type, mapper =>
                    mapper.MapFrom(publication => publication.Type.Name))
                .ForMember(card => card.AgeLimit, mapper =>
                    mapper.MapFrom(publication => publication.AgeLimit.Name))
                .ForMember(card => card.CoverArt, mapper =>
                    mapper.MapFrom(publication => publication.CoverArt.Name))
                .ForMember(card => card.Genres, mapper =>
                    mapper.MapFrom(publication => publication.Genres.ToArray()));
        }
    }
}
