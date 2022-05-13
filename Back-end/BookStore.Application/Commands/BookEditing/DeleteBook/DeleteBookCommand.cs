using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Persistance;
using BookStore.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using BookStore.Domain.Entities.Books;
using BookStore.Application.Commands.BookEditing.Common;
using BookStore.Application.Extensions;

namespace BookStore.Application.Commands.BookEditing.DeleteBook;

public record DeleteBookCommand(int Id) : IRequest;

internal class DeleteBookCommandHandler : AsyncRequestHandler<DeleteBookCommand>
{
    private ImageFileRepository ImageRepository { get; }
    private ApplicationContext Context { get; }

    public DeleteBookCommandHandler(
        ApplicationContext context,
        ImageFileRepository imageRepository)
    {
        Context = context;
        ImageRepository = imageRepository;
    }

    protected override async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var bookById = await Context.Books
            .Include(book => book.Album)
                .ThenInclude(album => album.Images)
            .SingleOrDefaultAsync(book => book.Id == request.Id, cancellationToken);

        if (bookById == null)
        {
            throw new NotFoundException(nameof(Book));
        }

        await ImageRepository.RemoveImagesFiles(bookById.Album.Images, cancellationToken);

        try
        {
            Context.Books.Remove(bookById);
            await Context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception exception)
        {
            throw new BadRequestException(exception.GetFullMessage());
        }
    }
}
