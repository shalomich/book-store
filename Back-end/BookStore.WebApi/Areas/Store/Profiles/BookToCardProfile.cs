
using BookStore.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Entities.Books;
using BookStore.WebApi.Areas.Store.ViewModels.Cards;

namespace BookStore.WebApi.Areas.Store.Profiles
{
    public class BookToCardProfile : Profile
    {
        public BookToCardProfile()
        {
            CreateMap<Book, ProductCard>();
            CreateMap<Book, BookCard>()
                .ForMember(card => card.PublisherName, mapper =>
                    mapper.MapFrom(book => book.Publisher.Name))
                .ForMember(card => card.AuthorName, mapper =>
                    mapper.MapFrom(book => book.Author.Name))
                .ForMember(card => card.Type, mapper =>
                    mapper.MapFrom(book => book.Type.Name))
                .ForMember(card => card.AgeLimit, mapper =>
                    mapper.MapFrom(book => book.AgeLimit.Name))
                .ForMember(card => card.CoverArt, mapper =>
                    mapper.MapFrom(book => book.CoverArt.Name))
                .ForMember(card => card.Genres, mapper =>
                    mapper.MapFrom(book => book.Genres.ToArray()));
        }
    }
}
