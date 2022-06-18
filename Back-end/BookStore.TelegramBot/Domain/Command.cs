namespace BookStore.TelegramBot.Domain;
internal class Command
{
    public int Id { get; set; }
    public string Text { get; set; }
    public TelegramBotUser User { get; set; }
    public int UserId { get; set; }
}

