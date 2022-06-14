using AutoMapper;
using BookStore.Application.Commands.BookEditing.Common;
using BookStore.Application.Commands.Selection.Common;
using BookStore.Application.Dto;
using BookStore.Domain.Entities.Books;
using BookStore.Persistance;
using QueryWorker;
using System;
using System.Linq;

namespace BookStore.Application.Commands.Selection.GetCurrentDayAuthorSelection;

public record GetCurrentDayAuthorSelectionQuery(OptionParameters OptionParameters) : GetSelectionQuery(OptionParameters);
internal class GetCurrentDayAuthorSelectionQueryHandler : GetSelectionQueryHandler<GetCurrentDayAuthorSelectionQuery>
{
    public ApplicationContext Context { get; }

    public GetCurrentDayAuthorSelectionQueryHandler(
        SelectionConfigurator<Book> selectionConfigurator,
        IMapper mapper,
        ImageFileRepository imageFileRepository,
        ApplicationContext context) : base(selectionConfigurator, mapper, imageFileRepository)
    {
        Context = context;
    }

    protected override IQueryable<Book> GetSelectionQuery(GetCurrentDayAuthorSelectionQuery request)
    {
        return Context.Books
            .Where(book => book.Author.SelectionOrder.SelectionDate.Date == DateTimeOffset.UtcNow.Date)
            .OrderByDescending(book => book.AddingDate);
    }
}

