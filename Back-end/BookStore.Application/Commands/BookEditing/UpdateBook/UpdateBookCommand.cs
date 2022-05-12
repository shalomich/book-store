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
            .Include(book => book.Discount)
            .Include(book => book.Album)
                .ThenInclude(album => album.Images)
            .SingleOrDefaultAsync(book => book.Id == id, cancellationToken);

        if (bookById == null)
        {
            throw new NotFoundException(nameof(Book));
        }

        var updatedBook = Mapper.Map(bookForm, bookById);

        try
        {
            await Context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception exception)
        {
            throw new BadRequestException(exception.InnerException.Message);
        }

        await UpdateImageFiles(bookById, updatedBook, cancellationToken);

        await Mediator.Publish(new BookUpdatedNotification(id));

        if (updatedBook.Discount.Percentage != bookById.Discount.Percentage
            && updatedBook.Discount.Percentage != 0)
        {
            await Mediator.Publish(new DiscountUpdatedNotification(id));
        }
    }

    private async Task UpdateImageFiles(Book oldBook, Book newBook, CancellationToken cancellationToken)
    {
        var oldImages = oldBook.Album.Images;
        var newImages = newBook.Album.Images;

        var imagesToAdd = newImages.Except(oldImages);
        var imagesToRemove = oldImages.Except(newImages);

        await ImageRepository.AddImageFiles(imagesToAdd, newBook.Id, cancellationToken);
        await ImageRepository.RemoveImagesFiles(imagesToRemove, newBook.Id, cancellationToken);
    }
}
