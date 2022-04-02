namespace BookStore.Domain.Entities;

public class Subscription : IEntity
{
    public int Id { get; set; }
    public long? TelegramId { get; set; }
    public bool MarkNotificationEnable { get; set; } = true;
    public bool TagNotificationEnable { get; set; } = true;
    public User User { get; set; }
    public int UserId { get; set; }
}

