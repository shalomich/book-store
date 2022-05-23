using BookStore.Application.Queries.Battle.GetBattleSettings;
using BookStore.Application.Services;
using BookStore.Domain.Entities.Battles;
using BookStore.Domain.Entities.Books;
using BookStore.Domain.Enums;
using BookStore.Persistance;
using System.Linq;

namespace BookStore.Application.Commands.Battles.StartBattle;
internal static class BattleProvider
{
    public static IQueryable<Book> GetBattleBooks(ApplicationContext context, BattleSettings battleSettings)
    {
        var previousBooksForBattle = context.Set<BattleBook>()
            .Select(battleBook => battleBook.Book);

        return context.Books
            .Except(previousBooksForBattle)
            .Where(book => book.Cost >= battleSettings.LowerBoundBookCost)
            .Where(book => book.OrderProducts
                .Any(orderProduct => orderProduct.Order.State == OrderState.Delivered))
            .Select(book => new
            {
                Book = book,
                ViewCount = book.Views.Sum(view => view.Count),
                OrderCount = book.OrderProducts.Sum(orderProduct => orderProduct.Quantity)
            })
            .OrderBy(bookInfo => bookInfo.ViewCount / bookInfo.OrderCount)
            .Select(bookInfo => bookInfo.Book);
    }
}

