namespace BookStore.Domain.Entities;
public abstract class RelatedEntity : IFormEntity
{
    public int Id { set; get; }
    public string Name { set; get; }
}
