namespace BookStore.TelegramBot.Notifications.NotifyByDiscountUpdated;

internal record NotifyByDiscountUpdatedMessageData
{
    public string BookName { get; init; }
    public string AuthorName { get; init; }
    public int DiscountPercentage { get; init; }
}

