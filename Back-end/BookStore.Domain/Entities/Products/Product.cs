using System;
using System.Collections.Generic;

namespace BookStore.Domain.Entities.Products;
public abstract class Product : IFormEntity
{
    public const int MinCost = 100;

    public const int MinQuantity = 0;
    public const int MaxQuantity = int.MaxValue;

    public const int MaxDescriptionLength = 1000;

    public int Id { set; get; }
    public string Name { set; get; }
    public int Cost { set; get; }
    public int Quantity { set; get; }
    public string Description { set; get; }
    public DateTimeOffset AddingDate { private set; get; } = DateTimeOffset.Now;

    public Album Album { set; get; }
    public Discount Discount { set; get; }
    public ProductCloseout ProductCloseout { set; get; }

    public IEnumerable<ProductTag> ProductTags { set; get; } = new List<ProductTag>();
    public IEnumerable<Mark> Marks { set; get; } = new List<Mark>();

    public IEnumerable<BasketProduct> BasketProducts { set; get; } = new List<BasketProduct>();
    public IEnumerable<OrderProduct> OrderProducts { set; get; } = new List<OrderProduct>();
}
