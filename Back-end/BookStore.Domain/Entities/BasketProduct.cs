using BookStore.Domain.Entities.Products;

namespace BookStore.Domain.Entities;
public class BasketProduct : IEntity
{
    public int Id { set; get; }
    public int Quantity { set; get; } = 1;

    public User User { set; get; }
    public int UserId { set; get; }
    public Product Product { set; get; }
    public int ProductId { set; get; }
}
