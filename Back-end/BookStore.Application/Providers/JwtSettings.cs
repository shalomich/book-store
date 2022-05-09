namespace BookStore.Application.Providers
{
    public record JwtSettings
    {
        public string TokenKey { init; get; }
        public int ExpiredMinutes { init; get; }
    }
}
