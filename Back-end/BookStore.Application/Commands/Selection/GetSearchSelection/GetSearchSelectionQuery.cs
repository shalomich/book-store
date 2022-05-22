using AutoMapper;
using BookStore.Application.Commands.BookEditing.Common;
using BookStore.Application.Commands.Selection.Common;
using BookStore.Application.Dto;
using BookStore.Domain.Entities.Books;
using BookStore.Persistance;
using QueryWorker;
using QueryWorker.Args;
using System.Linq;

namespace BookStore.Application.Commands.Selection.GetSearchSelection;

public record GetSearchSelectionQuery(SearchArgs SearchArgs, OptionParameters OptionParameters) : GetSelectionQuery(OptionParameters);
internal class GetSearchSelectionQueryHandler : GetSelectionQueryHandler<GetSearchSelectionQuery>
{
    private SelectionConfigurator<Book> SelectionConfigurator { get; }
    private ApplicationContext Context { get; }
    public GetSearchSelectionQueryHandler(
        SelectionConfigurator<Book> selectionConfigurator,
        IMapper mapper,
        ImageFileRepository imageFileRepository,
        ApplicationContext context) : base(selectionConfigurator, mapper, imageFileRepository)
    {
        SelectionConfigurator = selectionConfigurator;
        Context = context;
    }

    protected override IQueryable<Book> GetSelectionQuery(GetSearchSelectionQuery request)
    {
        return SelectionConfigurator.AddSearch(Context.Books, request.SearchArgs);
    }
}

