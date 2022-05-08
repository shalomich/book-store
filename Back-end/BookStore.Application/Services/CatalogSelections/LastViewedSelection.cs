using BookStore.Domain.Entities.Books;
using BookStore.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BookStore.Application.Services.CatalogSelections;
public class LastViewedSelection : IBookSelection
{
    private LoggedUserAccessor LoggedUserAccessor { get; }
    private ApplicationContext Context { get; }

    public LastViewedSelection(LoggedUserAccessor loggedUserAccessor, ApplicationContext context)
    {
        LoggedUserAccessor = loggedUserAccessor;
        Context = context;
    }

    public IQueryable<Book> Select()
    {
        var currentUserId = LoggedUserAccessor.GetCurrentUserId();

        return Context.Views
            .Where(view => view.UserId == currentUserId)
            .OrderByDescending(view => view.LastTime)
            .Select(view => view.Book);
    }
}

