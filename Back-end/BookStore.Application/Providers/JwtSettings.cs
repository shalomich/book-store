namespace BookStore.Application.Providers
{
    public record JwtSettings
    {
        public string TokenKey { init; get; }
        public int AccessTokenExpiredMinutes { init; get; }
        public int RefreshTokenExpiredMinutes { init; get; }
    }
}
