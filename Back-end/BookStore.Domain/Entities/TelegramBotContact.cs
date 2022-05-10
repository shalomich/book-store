namespace BookStore.Domain.Entities;

public class TelegramBotContact : IEntity
{
    public int Id { get; set; }
    public long TelegramUserId { get; set; }
    public bool IsAuthenticated { get; set; } = false;
    public User User { get; set; }
    public int UserId { get; set; }
}

