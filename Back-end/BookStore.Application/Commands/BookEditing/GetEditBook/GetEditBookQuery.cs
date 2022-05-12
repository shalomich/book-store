using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Application.Commands.BookEditing.Common;
using BookStore.Application.Exceptions;
using BookStore.Domain.Entities.Books;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.BookEditing.GetEditBook;

public record GetEditBookQuery(int Id) : IRequest<BookForm>;
internal class GetEditBookQueryHandler : IRequestHandler<GetEditBookQuery, BookForm>
{
    public IMapper Mapper { get; }
    public ApplicationContext Context { get; }

    public GetEditBookQueryHandler(
        IMapper mapper,
        ApplicationContext context)
    {
        Mapper = mapper;
        Context = context;
    }

    public async Task<BookForm> Handle(GetEditBookQuery request, CancellationToken cancellationToken)
    {
        var bookForm = await Context.Books
            .ProjectTo<BookForm>(Mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(book => book.Id == request.Id);

        if (bookForm == null)
        {
            throw new NotFoundException(nameof(Book));
        }

        return bookForm;
    }
}

