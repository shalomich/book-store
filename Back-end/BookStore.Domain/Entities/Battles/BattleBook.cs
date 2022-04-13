using BookStore.Domain.Entities.Books;
using System.Collections.Generic;

namespace BookStore.Domain.Entities.Battles;
public class BattleBook : IEntity
{
    public int Id { get; set; }
    public Book Book { get; set; }
    public int? BookId { get; set; }

    public Battle Battle { get; set; }
    public int BattleId { get; set; }

    public IEnumerable<Vote> Votes { get; set; } = new List<Vote>();
}

