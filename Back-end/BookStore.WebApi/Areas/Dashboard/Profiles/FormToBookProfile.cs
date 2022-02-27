
using BookStore.Domain.Entities;
using BookStore.Domain.Entities.Books;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.WebApi.Areas.Dashboard.ViewModels.Forms;
using BookStore.Domain.Entities.Products;

namespace BookStore.WebApi.Areas.Dashboard.Profiles
{
    public class FormToBookProfile : Profile
    {
        public FormToBookProfile()
        {
            CreateMap<BookForm, Book>()
              .ForMember(book => book.GenresBooks,
                mapper => mapper.MapFrom(form => form.GenreIds
                  .Select(id => new GenreBook { GenreId = id })))
              .ForMember(book => book.ProductTags,
                mapper => mapper.MapFrom(form => form.TagIds
                  .Select(id => new ProductTag { TagId = id })))
            .ReverseMap()
              .ForMember(form => form.GenreIds,
                mapper => mapper.MapFrom(book => book.GenresBooks
                  .Select(genre => genre.GenreId)))
              .ForMember(form => form.TagIds,
                mapper => mapper.MapFrom(book => book.ProductTags
                  .Select(bookTag => bookTag.TagId)));
              

            CreateMap<RelatedEntityForm, Author>()
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

            CreateMap<RelatedEntityForm, Tag>()
              .ReverseMap();
        }
    }
}
