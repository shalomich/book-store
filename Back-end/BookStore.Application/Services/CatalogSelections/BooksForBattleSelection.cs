using BookStore.Domain.Entities.Battles;
using BookStore.Domain.Entities.Books;
using BookStore.Persistance;
using System.Linq;

namespace BookStore.Application.Services.CatalogSelections;
public class BooksForBattleSelection : IBookSelection
{
    private ApplicationContext Context { get; }
    private BattleSettingsProvider BattleSettingsProvider { get; }

    public BooksForBattleSelection(ApplicationContext context, BattleSettingsProvider battleSettingsProvider)
    {
        Context = context;
        BattleSettingsProvider = battleSettingsProvider;
    }

    public IQueryable<Book> Select()
    {
        var battleSettings = BattleSettingsProvider.GetBattleSettings();

        var previousBooksForBattle = Context.Set<BattleBook>()
            .Select(battleBook => battleBook.Book);

        return Context.Books
            .Except(previousBooksForBattle)
            .Where(book => book.Cost >= battleSettings.LowerBoundBookCost)
            .Select(book => new
            {
                Book = book,
                ViewCount = book.Views.Sum(view => view.Count),
                OrderCount = book.OrderProducts.Sum(orderProduct => orderProduct.Quantity)
            })
            .Where(bookInfo => bookInfo.OrderCount != 0)
            .OrderBy(bookInfo => bookInfo.ViewCount / bookInfo.OrderCount)
            .Select(bookInfo => bookInfo.Book);
    }
}

