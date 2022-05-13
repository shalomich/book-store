using AutoMapper;
using BookStore.Application.Commands.BookEditing.Common;
using BookStore.Application.Exceptions;
using BookStore.Application.Extensions;
using BookStore.Application.Notifications.BookCreated;
using BookStore.Domain.Entities.Books;
using BookStore.Persistance;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.BookEditing.CreateBook;

public record CreateBookCommand(BookForm BookForm) : IRequest<int>;
internal class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
{
    private ApplicationContext Context { get; }
    private ImageFileRepository ImageFileRepository { get; }
    public IMapper Mapper { get; }
    public IMediator Mediator { get; }

    public CreateBookCommandHandler(
        ApplicationContext context,
        ImageFileRepository imageFileRepository,
        IMapper mapper,
        IMediator mediator)
    {
        Context = context;
        ImageFileRepository = imageFileRepository;
        Mapper = mapper;
        Mediator = mediator;
    }

    public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = Mapper.Map<Book>(request.BookForm);

        try
        {
            await Context.AddAsync(book);
            await Context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception exception)
        {
            throw new BadRequestException(exception.GetFullMessage());
        }

        await ImageFileRepository.AddImageFiles(book.Album.Images, cancellationToken);

        await Mediator.Publish(new BookCreatedNotification(book.Id));

        return book.Id;
    }
}

