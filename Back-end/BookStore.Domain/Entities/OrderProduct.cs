using BookStore.Domain.Entities.Products;

namespace BookStore.Domain.Entities;
public class OrderProduct : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public int Cost { get; set; }

    public Order Order { get; set; }
    public int OrderId { get; set; }

    public Product Product { get; set; }
    public int ProductId { get; set; }
}

