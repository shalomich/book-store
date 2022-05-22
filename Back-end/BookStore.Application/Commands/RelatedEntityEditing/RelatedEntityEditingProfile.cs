using AutoMapper;
using BookStore.Application.Commands.RelatedEntityEditing.Common;
using BookStore.Domain.Entities;
using BookStore.Domain.Entities.Books;
using BookStore.Domain.Entities.Products;

namespace BookStore.Application.Commands.RelatedEntityEditing;
internal class RelatedEntityEditingProfile : Profile
{
    public RelatedEntityEditingProfile()
    {
        #region Product related entities

        CreateMap<RelatedEntityForm, RelatedEntity>()
            .IncludeAllDerived()
            .ReverseMap();

        CreateMap<TagForm, Tag>()
            .ReverseMap();

        CreateMap<RelatedEntityForm, TagGroup>()
            .ReverseMap();

        #endregion

        #region Book related entities

        CreateMap<AuthorForm, Author>()
            .ReverseMap();

        CreateMap<RelatedEntityForm, Publisher>()
            .ReverseMap();

        CreateMap<RelatedEntityForm, Genre>()
            .ReverseMap();

        CreateMap<RelatedEntityForm, CoverArt>()
            .ReverseMap();

        CreateMap<RelatedEntityForm, BookType>()
            .ReverseMap();

        CreateMap<RelatedEntityForm, AgeLimit>()
            .ReverseMap();

        #endregion
    }
}