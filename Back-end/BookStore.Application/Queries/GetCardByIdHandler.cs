using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Application.Commands.BookEditing.Common;
using BookStore.Application.Dto;
using BookStore.Application.Exceptions;
using BookStore.Application.Services;
using BookStore.Domain.Entities.Battles;
using BookStore.Domain.Entities.Books;
using BookStore.Domain.Entities.Products;
using BookStore.Domain.Enums;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Queries;
public record GetCardByIdQuery(int BookId) : IRequest<CardDto>;
internal class GetCardByIdHandler : IRequestHandler<GetCardByIdQuery, CardDto>
{
    private class GetCardByIdProfile : Profile
    {
        public GetCardByIdProfile()
        {
            CreateMap<Tag, BookCardTagDto>()
                .ForMember(dto => dto.GroupName, mapper =>
                    mapper.MapFrom(tag => tag.TagGroup.Name));

            CreateMap<Book, CardDto>()
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
    private ApplicationContext Context { get; }
    private IMapper Mapper { get; }
    private ImageFileRepository ImageFileRepository { get; }

    public GetCardByIdHandler(
        ApplicationContext context,
        IMapper mapper, 
        ImageFileRepository imageFileRepository)
    {
        Context = context;
        Mapper = mapper;
        ImageFileRepository = imageFileRepository;
    }
    public async Task<CardDto> Handle(GetCardByIdQuery request, CancellationToken cancellationToken)
    {
        var card = await Context.Books
            .ProjectTo<CardDto>(Mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(book => book.Id == request.BookId);

        if (card == null)
            throw new NotFoundException("Book does not exist by this id.");

        card = await SetFileUrls(card, cancellationToken);

        return card;
    }


    private async Task<CardDto> SetFileUrls(CardDto card, CancellationToken cancellationToken)
    {
        var notTitleImages = new List<ImageDto>();

        var titleImage = card.TitleImage with
        {
            FileUrl = await ImageFileRepository.GetPresignedUrlForViewing(card.TitleImage.Id, cancellationToken)
        };

        foreach (var notTitleImage in card.NotTitleImages)
        {
            var notTitleImageWithFileUrl = notTitleImage with
            {
                FileUrl = await ImageFileRepository.GetPresignedUrlForViewing(notTitleImage.Id, cancellationToken)
            };

            notTitleImages.Add(notTitleImageWithFileUrl);
        }

        return card with { TitleImage = titleImage, NotTitleImages = notTitleImages.ToHashSet() };
    }
}

