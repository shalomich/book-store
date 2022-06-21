namespace BookStore.Bot.Exceptions;
internal class UnauthorizedException : Exception
{
    public UnauthorizedException(string? message) : base(message)
    {
    }
}

