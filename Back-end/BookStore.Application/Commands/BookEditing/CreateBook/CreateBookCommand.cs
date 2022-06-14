using AutoMapper;
using BookStore.Application.Commands.BookEditing.Common;
using BookStore.Application.Exceptions;
using BookStore.Application.Extensions;
using BookStore.Application.Notifications.BookCreated;
using BookStore.Domain.Entities.Books;
using BookStore.Persistance;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.BookEditing.CreateBook;

public record CreateBookCommand(BookForm BookForm) : IRequest<int>;
internal class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
{
    private ApplicationContext Context { get; }
    private ImageFileRepository ImageFileRepository { get; }
    private IMapper Mapper { get; }
    private IMediator Mediator { get; }
    private ILogger<CreateBookCommand> Logger { get; }

    public CreateBookCommandHandler(
        ApplicationContext context,
        ImageFileRepository imageFileRepository,
        IMapper mapper,
        IMediator mediator,
        ILogger<CreateBookCommand> logger)
    {
        Context = context;
        ImageFileRepository = imageFileRepository;
        Mapper = mapper;
        Mediator = mediator;
        Logger = logger;
    }

    public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = Mapper.Map<Book>(request.BookForm);

        await using var transaction = await Context.Database.BeginTransactionAsync(cancellationToken);
        
        try
        {
            await Context.AddAsync(book);
            await Context.SaveChangesAsync(cancellationToken);

            Logger.LogInformation("Adding to database book {BookName} is successful.", book.Name); 
        }
        catch (Exception exception)
        {
            Logger.LogError(exception, "Fail of adding to database book {BookName}", book.Name);

            throw new BadRequestException(exception.GetFullMessage());
        }

        try
        {
            await ImageFileRepository.AddImageFiles(book.Album.Images, cancellationToken);

            Logger.LogInformation("Adding to s3 book {BookName} is successful.", book.Name);
        }
        catch (Exception exception)
        {
            Logger.LogError(exception, "Fail of adding to s3 book {BookName}", book.Name);

            throw new BadRequestException(exception.GetFullMessage());
        }

        await transaction.CommitAsync(cancellationToken);
        
        await Mediator.Publish(new BookCreatedNotification(book.Id));

        return book.Id;
    }
}

