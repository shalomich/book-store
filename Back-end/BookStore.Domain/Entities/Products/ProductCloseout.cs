using System;

namespace BookStore.Domain.Entities.Products;
public class ProductCloseout : IEntity
{
    public int Id { set; get; }
    public DateTimeOffset Date { set; get; }
    public DateTimeOffset? ReplenishmentDate { set; get; }
    public Product Product { set; get; }
    public int ProductId { set; get; }
}
