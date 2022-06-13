namespace BookStore.Domain.Entities.Books;
public class Author : RelatedEntity
{
        
    public const string NameMask = "^[А-ЯЁ][а-яё]* [А-ЯЁ][а-яё]*( [А-ЯЁ][а-яё]*)?$";
    public const string NameSchema = "Surname Firstname Patronymic?";
    public AuthorSelectionOrder SelectionOrder { set; get; }
}

