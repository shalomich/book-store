namespace BookStore.TelegramBot.UseCases.TryConnectToStore;
internal enum AuthenticateTelegramUserStatus
{
    Ready,
    HasNoPhone,
    DifferentPhones,
    Success
}

