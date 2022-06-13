namespace BookStore.Domain.Entities.Products;
public class ProductTag : IEntity
{
    public int Id { set; get; }

    public Product Product { set; get; }
    public int ProductId { set; get; }

    public Tag Tag { set; get; }
    public int TagId { set; get; }
}
