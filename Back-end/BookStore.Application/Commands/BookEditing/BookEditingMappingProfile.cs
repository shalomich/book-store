using AutoMapper;
using BookStore.Application.Commands.BookEditing.Common;
using BookStore.Application.Commands.BookEditing.UpdateBook;
using BookStore.Domain.Entities.Books;
using BookStore.Domain.Entities.Products;
using System.Linq;

namespace BookStore.Application.Commands.BookEditing;
internal class BookEditingMappingProfile : Profile
{
    public BookEditingMappingProfile()
    {
        CreateMap<AlbumForm, Album>()
            .ReverseMap();

        CreateMap<ImageForm, Image>()
            .ReverseMap();

        CreateMap<DiscountForm, Discount>()
            .ReverseMap();

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
    }
}

