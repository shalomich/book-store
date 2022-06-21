using BookStore.Application.Notifications.DiscountUpdated;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace BookStore.Bot.Notifications.NotifyByDiscountUpdated;
internal class NotifyByDiscountUpdatedHandler : INotificationHandler<DiscountUpdatedNotification>
{
    private readonly string _notifyByDiscountUpdatedMessageTemplate;
    private ApplicationContext Context { get; }
    private ITelegramBotClient BotClient { get; }

    public NotifyByDiscountUpdatedHandler(ApplicationContext context, ITelegramBotClient botClient,
        IOptions<TelegramBotMessages> telegramBotMessages)
    {
        Context = context;
        BotClient = botClient;

        _notifyByDiscountUpdatedMessageTemplate = telegramBotMessages.Value.NotifyByDiscountUpdatedMessage;
    }
    public async Task Handle(DiscountUpdatedNotification notification, CancellationToken cancellationToken)
    {
        var discountUpdatedBookInfo = await GetDiscountUpdatedBookInfo(notification.ProductId, cancellationToken);

        if (discountUpdatedBookInfo.DiscountPercentage.Value == 0)
        {
            return;
        }

        var markSubsriberTelegramIds = await GetMarkSubsriberTelegramIds(discountUpdatedBookInfo.MarkIds, cancellationToken);

        var messageData = new NotifyByDiscountUpdatedMessageData
        {
            BookName = discountUpdatedBookInfo.BookName,
            AuthorName = discountUpdatedBookInfo.AuthorName,
            DiscountPercentage = discountUpdatedBookInfo.DiscountPercentage.Value
        };

        foreach (var telegramId in markSubsriberTelegramIds)
        {
            await NotifyByDiscountUpdated(telegramId, messageData);
        }

    }

    private async Task<(string BookName, string AuthorName, int? DiscountPercentage, int[] MarkIds)> GetDiscountUpdatedBookInfo(int bookId, CancellationToken cancellationToken)
    {
        var discountUpdatedBookInfo = await Context.Books
            .Where(book => book.Id == bookId)
            .Select(book => new
            {
                BookName = book.Name,
                AuthorName = book.Author.Name,
                DiscountPercentage = book.Discount.Percentage,
                MarkIds = book.Marks.Select(mark => mark.Id).ToArray()
            })
            .SingleAsync(cancellationToken);

        return (discountUpdatedBookInfo.BookName, discountUpdatedBookInfo.AuthorName, 
            discountUpdatedBookInfo.DiscountPercentage, discountUpdatedBookInfo.MarkIds);
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

    private async Task NotifyByDiscountUpdated(long telegramId, NotifyByDiscountUpdatedMessageData messageData)
    {
        var message = string.Format(_notifyByDiscountUpdatedMessageTemplate, messageData.BookName,
            messageData.AuthorName);

        await BotClient.SendTextMessageAsync(telegramId, message);
    }
}