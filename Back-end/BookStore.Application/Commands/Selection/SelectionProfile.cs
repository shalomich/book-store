using AutoMapper;
using BookStore.Application.Commands.Selection.Common;
using BookStore.Domain.Entities.Books;
using System.Linq;

namespace BookStore.Application.Profiles;
internal class SelectionProfile : Profile
{
    public SelectionProfile()
    {
        CreateMap<Book, PreviewDto>()
            .ForMember(card => card.TitleImage, mapper =>
            mapper.MapFrom(product => product.Album.Images
                .Single(image => image.Name == product.Album.TitleImageName)))
            .ForMember(card => card.PublisherName, mapper =>
            mapper.MapFrom(book => book.Publisher.Name))
            .ForMember(card => card.AuthorName, mapper =>
            mapper.MapFrom(book => book.Author.Name))
            .ForMember(card => card.DiscountPercentage, mapper =>
            mapper.MapFrom(book => book.Discount.Percentage));
    }
}

