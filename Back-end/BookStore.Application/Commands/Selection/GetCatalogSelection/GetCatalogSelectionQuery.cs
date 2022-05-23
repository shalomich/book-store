using AutoMapper;
using BookStore.Application.Commands.BookEditing.Common;
using BookStore.Application.Commands.Selection.Common;
using BookStore.Application.Dto;
using BookStore.Domain.Entities.Books;
using BookStore.Persistance;
using QueryWorker;
using System.Linq;

namespace BookStore.Application.Commands.Selection.GetCatalogSelection;

public record GetCatalogSelectionQuery(OptionParameters OptionParameters) : GetSelectionQuery(OptionParameters);
internal class GetCatalogSelectionQueryHandler : GetSelectionQueryHandler<GetCatalogSelectionQuery>
{
    private ApplicationContext Context { get; }
    public GetCatalogSelectionQueryHandler(
        SelectionConfigurator<Book> selectionConfigurator,
        IMapper mapper,
        ImageFileRepository imageFileRepository,
        ApplicationContext context) : base(selectionConfigurator, mapper, imageFileRepository)
    {
        Context = context;
    }

    protected override IQueryable<Book> GetSelectionQuery(GetCatalogSelectionQuery request)
    {
        return Context.Books;
    }
}

