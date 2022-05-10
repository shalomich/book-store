namespace BookStore.TelegramBot.UseCases.TryAuthenticateTelegramUser;
internal enum AuthenticateTelegramUserStatus
{
    Ready,
    HasNoPhone,
    DifferentPhones,
    Success
}

