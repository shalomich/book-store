using System;

namespace BookStore.Domain.Entities.Products;
public class Discount : IEntity
{
    public int Id { get; set; }
    public int Percentage { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public Product Product { get; set; }
    public int ProductId { get; set; }
}

