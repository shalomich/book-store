using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Persistance;
using BookStore.Application.Exceptions;
using AutoMapper;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BookStore.Domain.Entities.Books;
using BookStore.Application.Commands.BookEditing.Common;
using BookStore.Application.Notifications.BookUpdated;
using BookStore.Application.Notifications.DiscountUpdated;
using BookStore.Application.Extensions;
using System.Collections;
using System.Collections.Generic;
using BookStore.Domain.Entities.Products;

namespace BookStore.Application.Commands.BookEditing.UpdateBook;

public record UpdateBookCommand(int Id, BookForm BookForm) : IRequest;

internal class UpdateBookCommandHandler : AsyncRequestHandler<UpdateBookCommand>
{
    private IMapper Mapper { get; }
    private ImageFileRepository ImageRepository { get; }
    private ApplicationContext Context { get; }

    private IMediator Mediator { get; }
   
    public UpdateBookCommandHandler(
        ApplicationContext context,
        IMapper mapper,
        ImageFileRepository imageRepository,
        IMediator mediator)
    {
        Context = context;
        Mapper = mapper;
        ImageRepository = imageRepository;
        Mediator = mediator;
    }

    protected override async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var (id, bookForm) = request;

        var bookById = await Context.Books
            .Include(book => book.GenresBooks)
            .Include(book => book.ProductTags)
            .Include(book => book.Discount)
            .Include(book => book.Album)
                .ThenInclude(album => album.Images)
            .SingleOrDefaultAsync(book => book.Id == id, cancellationToken);

        if (bookById == null)
        {
            throw new NotFoundException(nameof(Book));
        }

        var oldImages = bookById.Album.Images.ToList();
        var oldDiscountPercentage = bookById.Discount?.Percentage;

        bookById = Mapper.Map(bookForm, bookById);

        await using var transaction = await Context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await Context.SaveChangesAsync(cancellationToken);

            await UpdateImageFiles(oldImages, bookById.Album.Images, cancellationToken);
        }
        catch (Exception exception)
        {
            throw new BadRequestException(exception.GetFullMessage());
        }

        await transaction.CommitAsync(cancellationToken);

        await Mediator.Publish(new BookUpdatedNotification(id));

        await CheckDiscountChange(oldDiscountPercentage, bookById.Discount?.Percentage, id, cancellationToken);
    }

    private async Task UpdateImageFiles(IEnumerable<Image> oldImages, IEnumerable<Image> newImages, CancellationToken cancellationToken)
    {
        await ImageRepository.AddImageFiles(newImages, cancellationToken);
        await ImageRepository.RemoveImagesFiles(oldImages, cancellationToken);
    }

    private async Task CheckDiscountChange(int? oldDiscountPercentage, int? newDiscountPercentage, int bookId, CancellationToken cancellationToken)
    {
        if (newDiscountPercentage.HasValue
            && newDiscountPercentage != 0
            && newDiscountPercentage != oldDiscountPercentage)
        {
            await Mediator.Publish(new DiscountUpdatedNotification(bookId), cancellationToken);
        }
    }
}
