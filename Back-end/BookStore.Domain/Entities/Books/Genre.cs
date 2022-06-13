using System.Collections.Generic;
namespace BookStore.Domain.Entities.Books;
public class Genre : RelatedEntity
{
    public IEnumerable<GenreBook> Books { set; get; } = new List<GenreBook>();
}

