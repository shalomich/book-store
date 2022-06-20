namespace BookStore.TelegramBot.Exceptions;
internal class UnauthorizedException : Exception
{
    public UnauthorizedException(string? message) : base(message)
    {
    }
}

