namespace BookStore.Domain.Entities.Books;
public class GenreBook : IEntity
{
    public int Id { set; get; }

    public Book Book { set; get; }
    public int BookId { set; get; }

    public Genre Genre { set; get; }
    public int GenreId { set; get; }
}
