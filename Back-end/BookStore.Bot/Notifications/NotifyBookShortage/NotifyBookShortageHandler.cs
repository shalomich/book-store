using BookStore.Application.Notifications.OrderPlaced;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace BookStore.Bot.Notifications.NotifyBookShortage;

internal class NotifyBookShortageHandler : INotificationHandler<OrderPlacedNotification>
{
    private readonly int _shortageBookQuantity;
    private readonly string _notifyBookShortageMessageTemplate;
    private readonly string _notifyBookAbsenceMessageTemplate;
    private ApplicationContext Context { get; }
    private ITelegramBotClient BotClient { get; }

    public NotifyBookShortageHandler(ApplicationContext context, ITelegramBotClient botClient,
        IOptions<TelegramBotMessages> telegramBotMessages)
    {
        Context = context;
        BotClient = botClient;

        _notifyBookShortageMessageTemplate = telegramBotMessages.Value.NotifyBookShortageMessage;
        _shortageBookQuantity = telegramBotMessages.Value.ShortageBookQuantity;
        _notifyBookAbsenceMessageTemplate = telegramBotMessages.Value.NotifyBookAbsenceMessage;
    }
    public async Task Handle(OrderPlacedNotification notification, CancellationToken cancellationToken)
    {
        var bookIds = await GetOrderBookIds(notification.OrderId, cancellationToken);

        await foreach (var shortageBookInfo in GetShortageBooksInfo(bookIds, cancellationToken))
        {
            var markSubsriberTelegramIds = await GetMarkSubsriberTelegramIds(shortageBookInfo.MarkIds, cancellationToken);

            var messageData = new NotifyBookShortageMessageData
            {
                BookName = shortageBookInfo.BookName,
                AuthorName = shortageBookInfo.AuthorName,
                BookQuantity = shortageBookInfo.BookQuantity
            };

            foreach (var telegramId in markSubsriberTelegramIds)
            {
                await NotifyBookShortage(telegramId, messageData);
            }
        }
    }

    private async Task<IEnumerable<int>> GetOrderBookIds(int orderId, CancellationToken cancellationToken)
    {
        return await Context.Orders
            .Where(order => order.Id == orderId)
            .SelectMany(order => order.Products)
            .Select(orderProduct => orderProduct.ProductId)
            .ToListAsync(cancellationToken);
    }
    private async IAsyncEnumerable<(string BookName, string AuthorName, int BookQuantity, int[] MarkIds)> GetShortageBooksInfo(IEnumerable<int> bookIds, CancellationToken cancellationToken)
    {
        var booksByIds = Context.Books
            .Where(book => bookIds.Contains(book.Id));

        var shortageBooks = booksByIds
            .Where(book => book.Quantity <= _shortageBookQuantity);

        var shortageBooksInfo = await shortageBooks
            .Select(book => new
            {
                BookName = book.Name,
                AuthorName = book.Author.Name,
                BookQuantity = book.Quantity,
                MarkIds = book.Marks.Select(mark => mark.Id).ToArray()
            })
            .ToListAsync(cancellationToken);

        foreach (var shortageBookInfo in shortageBooksInfo)
        {
            yield return (shortageBookInfo.BookName, shortageBookInfo.AuthorName, shortageBookInfo.BookQuantity, shortageBookInfo.MarkIds);
        }
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

    private async Task NotifyBookShortage(long telegramId, NotifyBookShortageMessageData messageData)
    {
        var message = messageData.BookQuantity == 0 
            ? string.Format(_notifyBookAbsenceMessageTemplate, messageData.BookName, messageData.AuthorName) 
            : string.Format(_notifyBookShortageMessageTemplate,
            messageData.BookQuantity, messageData.BookName, messageData.AuthorName);

        await BotClient.SendTextMessageAsync(telegramId, message);
    }
}

