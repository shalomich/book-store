namespace BookStore.Domain.Entities;

public class Subscription : IEntity
{
    public int Id { get; set; }
    public long? TelegramId { get; set; }
    public bool IsActive { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }

}

