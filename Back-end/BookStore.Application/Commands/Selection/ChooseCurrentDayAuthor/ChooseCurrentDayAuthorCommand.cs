using BookStore.Application.Exceptions;
using BookStore.Application.Extensions;
using BookStore.Domain.Entities.Books;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Selection.ChooseCurrentDayAuthor;

public record ChooseCurrentDayAuthorCommand() : IRequest;
internal class ChooseCurrentDayAuthorCommandHandler : AsyncRequestHandler<ChooseCurrentDayAuthorCommand>
{
    private ApplicationContext Context { get; }

    public ChooseCurrentDayAuthorCommandHandler(
        ApplicationContext context)
    {
        Context = context;
    }

    protected override async Task Handle(ChooseCurrentDayAuthorCommand request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);

        var newCurrentDayAuthorQuery = Context.Authors
            .Where(author => author.SelectionOrder == null)
            .Shuffle();
        
        var newCurrentDayAuthor = await newCurrentDayAuthorQuery
            .FirstOrDefaultAsync(cancellationToken);

        if (newCurrentDayAuthor == null)
        {
            Context.Set<AuthorSelectionOrder>()
                .RemoveRange(Context.Set<AuthorSelectionOrder>());

            await Context.SaveChangesAsync(cancellationToken);

            newCurrentDayAuthor = await newCurrentDayAuthorQuery
                .FirstOrDefaultAsync(cancellationToken);
        }

        newCurrentDayAuthor.SelectionOrder = new AuthorSelectionOrder();

        await Context.SaveChangesAsync(cancellationToken);
    }

    private async Task Validate(ChooseCurrentDayAuthorCommand request, CancellationToken cancellation)
    {
        var hasCurrentDayAuthor = await Context.Authors
            .AnyAsync(author => author.SelectionOrder.SelectionDate.Date == DateTimeOffset.UtcNow.Date, cancellation);

        if (hasCurrentDayAuthor)
        {
            throw new BadRequestException("Current day author has already choosen");
        }
    }
}

