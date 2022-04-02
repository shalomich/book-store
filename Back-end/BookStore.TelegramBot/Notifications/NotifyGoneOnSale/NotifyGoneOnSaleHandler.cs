using BookStore.Application.DbQueryConfigs.Specifications;
using BookStore.Application.Notifications.BookCreated;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace BookStore.TelegramBot.Notifications.NotifyGoneOnSale;

internal class NotifyGoneOnSaleHandler : INotificationHandler<BookCreatedNotification>
{
    private readonly string _notifyGoneOnSaleMessageTemplate;
    private ApplicationContext Context { get; }
    private ITelegramBotClient BotClient { get; }

    public NotifyGoneOnSaleHandler(ApplicationContext context, ITelegramBotClient botClient,
        IOptions<TelegramBotMessages> telegramBotMessages)
    {
        Context = context;
        BotClient = botClient;

        _notifyGoneOnSaleMessageTemplate = telegramBotMessages.Value.NotifyGoneOnSaleMessage;
    }

    public async Task Handle(BookCreatedNotification notification, CancellationToken cancellationToken)
    {
        var (bookName, authorName, tagIds) = await GetGoneOnSaleBookInfo(notification.BookId, cancellationToken);

        var subscribersInfo = await GetTagSubscribersInfo(tagIds, cancellationToken);

        foreach (var subscriberInfo in subscribersInfo)
        {
            await NotifyGoneOnSale(subscriberInfo.TelegramId,
                new NotifyGoneOnSaleMessageData
                {
                    Tags = subscriberInfo.Tags,
                    BookName = bookName,
                    AuthorName = authorName
                });
        }
    }

    private async Task<(string BookName, string AuthorName, IEnumerable<int> TagIds)> GetGoneOnSaleBookInfo(int bookId, CancellationToken cancellationToken)
    {
        var goneOnSaleBookInfo = await Context.Books
            .Where(book => book.Id == bookId)
            .Select(book => new
            {
                TagIds = book.ProductTags.Select(productTag => productTag.TagId),
                BookName = book.Name,
                AuthorName = book.Author.Name
            })
            .SingleAsync(cancellationToken);

        return (goneOnSaleBookInfo.BookName, goneOnSaleBookInfo.AuthorName, goneOnSaleBookInfo.TagIds);
    }

    private async Task<(long TelegramId, string[] Tags)[]> GetTagSubscribersInfo(IEnumerable<int> tagIds, CancellationToken cancellationToken)
    {
        var usersWithActiveSubsription = Context.Users
            .Where(new HasConfirmedSubsciptionSpecification())
            .Where(user => user.Subscription.TagNotificationEnable == true);

        var usersHaveTags = usersWithActiveSubsription
            .Where(user => user.Tags.Any(
                tag => tagIds.Contains(tag.Id)));

        var subscribersInfo = await usersHaveTags
            .Select(user => new
            {
                Tags = user.Tags
                    .Where(tag => tagIds.Contains(tag.Id))
                    .Select(tag => tag.Name)
                    .ToArray(),
                TelegramId = user.Subscription.TelegramId.Value
            })
            .ToListAsync(cancellationToken);

        return subscribersInfo
            .Select(subscriberInfo => (subscriberInfo.TelegramId, subscriberInfo.Tags))
            .ToArray();
    }

    private async Task NotifyGoneOnSale(long telegramId, NotifyGoneOnSaleMessageData messageData)
    {
        var tagsString = string.Join(", ", messageData.Tags);

        var message = string.Format(_notifyGoneOnSaleMessageTemplate,
            tagsString, messageData.BookName, messageData.AuthorName);

        await BotClient.SendTextMessageAsync(telegramId, message);
    }
}