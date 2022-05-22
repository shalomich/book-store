using AutoMapper;
using BookStore.Application.Commands.BookEditing.Common;
using BookStore.Application.Commands.Selection.Common;
using BookStore.Application.Dto;
using BookStore.Domain.Entities.Books;
using BookStore.Persistance;
using QueryWorker;
using System;
using System.Linq;

namespace BookStore.Application.Commands.Selection.GetNoveltySelection;

public record GetNoveltySelectionQuery(OptionParameters OptionParameters) : GetSelectionQuery(OptionParameters);
internal class GetNoveltySelectionQueryHandler : GetSelectionQueryHandler<GetNoveltySelectionQuery>
{
    public ApplicationContext Context { get; }

    public GetNoveltySelectionQueryHandler(
        SelectionConfigurator<Book> selectionConfigurator, 
        IMapper mapper, 
        ImageFileRepository imageFileRepository,
        ApplicationContext context) : base(selectionConfigurator, mapper, imageFileRepository)
    {
        Context = context;
    }

    protected override IQueryable<Book> GetSelectionQuery(GetNoveltySelectionQuery request)
    {
        return Context.Books
            .Where(book => book.ReleaseYear == DateTime.Now.Year)
            .OrderByDescending(book => book.AddingDate);
    }
}

