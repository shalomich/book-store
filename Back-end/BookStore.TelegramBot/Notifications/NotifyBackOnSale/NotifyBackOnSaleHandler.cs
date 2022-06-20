using BookStore.Application.Notifications.BookUpdated;
using BookStore.Domain.Entities.Products;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace BookStore.TelegramBot.Notifications.NotifyBackOnSale;

internal class NotifyBackOnSaleHandler : INotificationHandler<BookUpdatedNotification>
{
    private readonly string _notifyBackOnSaleMessageTemplate;
    private ApplicationContext Context { get; }
    private ITelegramBotClient BotClient { get; }

    public NotifyBackOnSaleHandler(ApplicationContext context, ITelegramBotClient botClient,
        IOptions<TelegramBotMessages> telegramBotMessages)
    {
        Context = context;
        BotClient = botClient;

        _notifyBackOnSaleMessageTemplate = telegramBotMessages.Value.NotifyBackOnSaleMessage;
    }
    public async Task Handle(BookUpdatedNotification notification, CancellationToken cancellationToken)
    {
        bool isBackOnSale = await CheckBackOnSale(notification.BookId, cancellationToken);

        if (!isBackOnSale)
            return;

        var backOnSaleBookInfo = await GetBackOnSaleBookInfo(notification.BookId, cancellationToken);

        var markSubsriberTelegramIds = await GetMarkSubsriberTelegramIds(backOnSaleBookInfo.MarkIds, cancellationToken);

        var messageData = new NotifyBackOnSaleMessageData
        {
            BookName = backOnSaleBookInfo.BookName,
            AuthorName = backOnSaleBookInfo.AuthorName,
        };

        foreach (var telegramId in markSubsriberTelegramIds)
        {
            await NotifyBookShortage(telegramId, messageData);
        }
    }

    private async Task<bool> CheckBackOnSale(int bookId, CancellationToken cancellationToken)
    {
        var updatedBookInfo = await Context.Books
            .Where(book => book.Id == bookId)
            .Select(book => new
            {
                book.Quantity,
                book.ProductCloseout
            })
            .SingleOrDefaultAsync(cancellationToken);
        
        var closeout = updatedBookInfo.ProductCloseout;

        if (updatedBookInfo.Quantity != 0 
            && closeout != null && closeout.ReplenishmentDate == null)
        {
            closeout.ReplenishmentDate = DateTime.Now;

            Context.Update(closeout);
            await Context.SaveChangesAsync(cancellationToken);

            return true;
        }

        return false;
    }

    private async Task<(string BookName, string AuthorName, int[] MarkIds)> GetBackOnSaleBookInfo(int bookId, CancellationToken cancellationToken)
    {
        var backOnSaleBookInfo = await Context.Books
            .Where(book => book.Id == bookId)
            .Select(book => new
            {
                BookName = book.Name,
                AuthorName = book.Author.Name,
                MarkIds = book.Marks.Select(mark => mark.Id).ToArray()
            })
            .SingleAsync(cancellationToken);

        return (backOnSaleBookInfo.BookName, backOnSaleBookInfo.AuthorName, backOnSaleBookInfo.MarkIds);
    }

    private async Task<IEnumerable<long>> GetMarkSubsriberTelegramIds(IEnumerable<int> markIds, CancellationToken cancellationToken)
    {
        var usersWithActiveSubsription = Context.Users
            .Where(user => user.TelegramBotContact.IsAuthenticated == true);

        var usersHaveMark = usersWithActiveSubsription
            .Where(user => user.Marks.Any(
                mark => markIds.Contains(mark.Id)));

        return await usersHaveMark
            .Select(user => user.TelegramBotContact.TelegramUserId)
            .ToListAsync(cancellationToken);
    }

    private async Task NotifyBookShortage(long telegramId, NotifyBackOnSaleMessageData messageData)
    {
        var message = string.Format(_notifyBackOnSaleMessageTemplate, messageData.BookName, 
            messageData.AuthorName);

        await BotClient.SendTextMessageAsync(telegramId, message);
    }
}

