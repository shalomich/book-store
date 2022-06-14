using System;

namespace BookStore.Domain.Entities.Books;
public class AuthorSelectionOrder : IEntity
{
    public int Id { set; get; }
    public DateTimeOffset SelectionDate { private set; get; } = DateTimeOffset.Now;
    public Author Author { set; get; }
    public int AuthorId { set; get; }
}

