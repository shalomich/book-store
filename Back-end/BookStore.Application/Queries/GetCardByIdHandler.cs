using AutoMapper;
using AutoMapper.QueryableExtensions;
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
                        .Select(genreBook => genreBook.Genre.Name)));
        }
    }

    private LoggedUserAccessor LoggedUserAccessor { get; }
    private ApplicationContext Context { get; }
    private IMapper Mapper { get; }
    private S3Storage S3Storage { get; }

    public GetCardByIdHandler(LoggedUserAccessor loggedUserAccessor, ApplicationContext context,
        IMapper mapper, S3Storage s3Storage)
    {
        LoggedUserAccessor = loggedUserAccessor;
        Context = context;
        Mapper = mapper;
        S3Storage = s3Storage;
    }
    public async Task<CardDto> Handle(GetCardByIdQuery request, CancellationToken cancellationToken)
    {
        var card = await Context.Books
            .ProjectTo<CardDto>(Mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(book => book.Id == request.BookId);

        if (card == null)
            throw new NotFoundException("Book does not exist by this id.");

        card = SetFileUrls(card);

        card = await SetBattleStatus(card, cancellationToken);

        if (LoggedUserAccessor.IsAuthenticated())
        {
            int currentUserId = LoggedUserAccessor.GetCurrentUserId();

            card = await SetBasketStatus(card, currentUserId, cancellationToken);
            card = await SetMarkStatus(card, currentUserId, cancellationToken);
        }

        return card;
    }


    private CardDto SetFileUrls(CardDto card)
    {
        var notTitleImages = new List<ImageDto>();

        var titleImage = card.TitleImage with
        {
            FileUrl = S3Storage.GetPresignedUrlForViewing(card.Id, card.TitleImage.Id)
        };

        foreach (var notTitleImage in card.NotTitleImages)
        {
            var notTitleImageWithFileUrl = notTitleImage with
            {
                FileUrl = S3Storage.GetPresignedUrlForViewing(card.Id, notTitleImage.Id)
            };

            notTitleImages.Add(notTitleImageWithFileUrl);
        }

        return card with { TitleImage = titleImage, NotTitleImages = notTitleImages.ToHashSet() };
    }

    private async Task<CardDto> SetBasketStatus(CardDto card, int currentUserId, CancellationToken cancellationToken)
    {
        bool isInBasket = await Context.BasketProducts
            .AnyAsync(basketProduct => basketProduct.UserId == currentUserId
                && basketProduct.ProductId == card.Id, cancellationToken);

        return card with { IsInBasket = isInBasket };
    }

    private async Task<CardDto> SetMarkStatus(CardDto card, int currentUserId, CancellationToken cancellationToken)
    {
        bool isMarked = await Context.Set<Mark>()
            .AnyAsync(mark => mark.UserId == currentUserId
                && mark.ProductId == card.Id, cancellationToken);

        return card with { IsMarked = isMarked };
    }

    private async Task<CardDto> SetBattleStatus(CardDto card, CancellationToken cancellationToken)
    {
        bool isInBattle = await Context.Set<BattleBook>()
            .AnyAsync(battleBook => battleBook.Battle.State != BattleState.Finished
                && battleBook.BookId == card.Id, cancellationToken);

        return card with { IsInBattle = isInBattle };
    }
}

