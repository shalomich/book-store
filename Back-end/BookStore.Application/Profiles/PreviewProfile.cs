using AutoMapper;
using BookStore.Application.Dto;
using BookStore.Domain.Entities.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Profiles
{
    internal class PreviewProfile : Profile
    {
        public PreviewProfile()
        {
            CreateMap<Book, PreviewDto>()
              .ForMember(card => card.TitleImage, mapper =>
                mapper.MapFrom(product => product.Album.TitleImage))
              .ForMember(card => card.PublisherName, mapper =>
                mapper.MapFrom(book => book.Publisher.Name))
              .ForMember(card => card.AuthorName, mapper =>
                mapper.MapFrom(book => book.Author.Name));
        }
    }
}
