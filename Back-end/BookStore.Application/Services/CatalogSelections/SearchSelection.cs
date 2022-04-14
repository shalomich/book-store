using BookStore.Domain.Entities.Books;
using Microsoft.EntityFrameworkCore;
using QueryWorker;
using QueryWorker.Args;
using System.Linq;

namespace BookStore.Application.Services.CatalogSelections;
public class SearchSelection : ICatalogSelection
{
    private SelectionConfigurator<Book> SelectionConfigurator { get; }
    public SearchArgs SearchArgs { get; set; }

    public SearchSelection(SelectionConfigurator<Book> selectionConfigurator)
    {
        SelectionConfigurator = selectionConfigurator;
    }

    public IQueryable<Book> Select(DbSet<Book> bookSet)
    {
        IQueryable<Book> searchBooks = bookSet;

        if (SearchArgs != null)
            searchBooks = SelectionConfigurator.AddSearch(searchBooks, SearchArgs);

        return searchBooks;
    }
}
