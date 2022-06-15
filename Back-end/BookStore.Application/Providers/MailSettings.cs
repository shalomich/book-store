namespace BookStore.Application.Providers;
public record MailSettings
{
    public string SenderName { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public string Host { get; init; }
    public int Port { get; init; }
}

