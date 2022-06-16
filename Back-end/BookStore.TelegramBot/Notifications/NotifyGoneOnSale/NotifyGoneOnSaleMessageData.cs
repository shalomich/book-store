namespace BookStore.TelegramBot.Notifications.NotifyGoneOnSale;

internal record NotifyGoneOnSaleMessageData
{
    public string BookName { get; init; }  
    public string AuthorName { get; init; }
    public IEnumerable<string> Tags { get; init; } = new List<string>();
}

