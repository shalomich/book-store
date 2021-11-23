using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Domain.Entities.Books;
using BookStore.Domain.Entities.Products;
using BookStore.WebApi.Areas.Store.ViewModels.Cards;

namespace BookStore.WebApi.Areas.Store.Profiles
{
    public class CardProfile : Profile
    {
        public CardProfile()
        {
            CreateMap<Product, ProductPreview>()
                .ForMember(card => card.TitleImage, mapper =>
                    mapper.MapFrom(product => product.Album.TitleImage))
                .IncludeAllDerived();
            
            CreateMap<Book, BookPreview>()
                .ForMember(card => card.PublisherName, mapper =>
                    mapper.MapFrom(book => book.Publisher.Name))
                .ForMember(card => card.AuthorName, mapper =>
                    mapper.MapFrom(book => book.Author.Name));

            CreateMap<Product, ProductCard>()
                .ForMember(card => card.TitleImage, mapper =>
                    mapper.MapFrom(product => product.Album.TitleImage))
                .ForMember(card => card.NotTitleImages, mapper =>
                    mapper.MapFrom(product => product.Album.NotTitleImages))
                .IncludeAllDerived();

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
