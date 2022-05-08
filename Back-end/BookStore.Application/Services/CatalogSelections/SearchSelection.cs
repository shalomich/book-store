using BookStore.Domain.Entities.Books;
using BookStore.Persistance;
using Microsoft.EntityFrameworkCore;
using QueryWorker;
using QueryWorker.Args;
using System.Linq;

namespace BookStore.Application.Services.CatalogSelections;
public class SearchSelection : IBookSelection
{
    private SelectionConfigurator<Book> SelectionConfigurator { get; }
    private ApplicationContext Context { get; }
    public SearchArgs SearchArgs { get; set; }

    public SearchSelection(SelectionConfigurator<Book> selectionConfigurator, ApplicationContext context)
    {
        SelectionConfigurator = selectionConfigurator;
        Context = context;
    }

    public IQueryable<Book> Select()
    {
        IQueryable<Book> searchBooks = Context.Books;

        if (SearchArgs != null)
            searchBooks = SelectionConfigurator.AddSearch(searchBooks, SearchArgs);

        return searchBooks;
    }
}
