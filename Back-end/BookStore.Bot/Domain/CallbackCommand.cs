namespace BookStore.Bot.Domain;
internal class CallbackCommand
{
    public int Id { get; set; }
    public string CommandLine { get; set; }
    public TelegramBotUser User { get; set; }
    public long UserId { get; set; }
}

