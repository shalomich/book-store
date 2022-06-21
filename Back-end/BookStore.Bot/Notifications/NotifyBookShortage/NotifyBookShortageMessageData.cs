namespace BookStore.Bot.Notifications.NotifyBookShortage;

internal record NotifyBookShortageMessageData
{
    public string BookName { get; init; }
    public string AuthorName { get; init; }
    public int BookQuantity { get; init; }
}

