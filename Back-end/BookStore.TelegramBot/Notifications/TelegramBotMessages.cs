namespace BookStore.TelegramBot.Notifications;

public record TelegramBotMessages
{
    public string NotifyGoneOnSaleMessage { get; init; }
    public int ShortageBookQuantity { get; init; }
    public string NotifyBookShortageMessage { get; init; }
    public string NotifyBookAbsenceMessage { get; init; }
    public string NotifyBackOnSaleMessage { get; init; }
}

