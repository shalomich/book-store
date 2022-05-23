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

namespace BookStore.Application.Commands.Catalog.GetBookCard;
public record GetCardByIdQuery(int BookId) : IRequest<BookCardDto>;
internal class GetBookCardQueryHandler : IRequestHandler<GetCardByIdQuery, BookCardDto>
{
    private ApplicationContext Context { get; }
    private IMapper Mapper { get; }
    private ImageFileRepository ImageFileRepository { get; }

    public GetBookCardQueryHandler(
        ApplicationContext context,
        IMapper mapper,
        ImageFileRepository imageFileRepository)
    {
        Context = context;
        Mapper = mapper;
        ImageFileRepository = imageFileRepository;
    }
    public async Task<BookCardDto> Handle(GetCardByIdQuery request, CancellationToken cancellationToken)
    {
        var card = await Context.Books
            .ProjectTo<BookCardDto>(Mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(book => book.Id == request.BookId);

        if (card == null)
            throw new NotFoundException("Book does not exist by this id.");

        card = await SetFileUrls(card, cancellationToken);

        return card;
    }


    private async Task<BookCardDto> SetFileUrls(BookCardDto card, CancellationToken cancellationToken)
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

