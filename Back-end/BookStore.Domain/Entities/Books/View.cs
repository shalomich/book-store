using System;

namespace BookStore.Domain.Entities.Books;
public class View : IEntity
{
    public int Id { get; set; }
    public int Count { get; set; } = 1;
    public DateTimeOffset LastViewDate { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset LastViewCountChangeDate { get; set; } = DateTimeOffset.Now;
    public Book Book { get; set; }
    public int BookId { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
}
