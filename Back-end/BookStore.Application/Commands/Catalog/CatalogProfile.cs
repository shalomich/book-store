using AutoMapper;
using BookStore.Application.Commands.Catalog.GetBookCard;
using BookStore.Domain.Entities.Books;
using BookStore.Domain.Entities.Products;
using System.Linq;

namespace BookStore.Application.Commands.Catalog;
internal class CatalogProfile : Profile
{
    public CatalogProfile()
    {
        CreateMap<Tag, BookCardTagDto>()
               .ForMember(dto => dto.GroupName, mapper =>
                   mapper.MapFrom(tag => tag.TagGroup.Name))
               .ForMember(dto => dto.ColorHex, mapper =>
                   mapper.MapFrom(tag => tag.TagGroup.ColorHex));

        CreateMap<Book, BookCardDto>()
            .ForMember(card => card.TitleImage, mapper =>
                mapper.MapFrom(product => product.Album.Images
                    .Single(image => image.Name == product.Album.TitleImageName)))
            .ForMember(card => card.NotTitleImages, mapper =>
                mapper.MapFrom(product => product.Album.Images
                    .Where(image => image.Name != product.Album.TitleImageName)))
            .ForMember(card => card.PublisherName, mapper =>
                mapper.MapFrom(book => book.Publisher.Name))
            .ForMember(card => card.AuthorName, mapper =>
                mapper.MapFrom(book => book.Author.Name))
            .ForMember(card => card.DiscountPercentage, mapper =>
                mapper.MapFrom(book => book.Discount.Percentage))
            .ForMember(card => card.Type, mapper =>
                mapper.MapFrom(book => book.Type.Name))
            .ForMember(card => card.AgeLimit, mapper =>
                mapper.MapFrom(book => book.AgeLimit.Name))
            .ForMember(card => card.CoverArt, mapper =>
                mapper.MapFrom(book => book.CoverArt.Name))
            .ForMember(card => card.Genres, mapper =>
                mapper.MapFrom(book => book.GenresBooks
                    .Select(genreBook => genreBook.Genre.Name)))
            .ForMember(card => card.Tags, mapper =>
                mapper.MapFrom(book => book.ProductTags
                    .Select(productTag => productTag.Tag)));
    }
}

