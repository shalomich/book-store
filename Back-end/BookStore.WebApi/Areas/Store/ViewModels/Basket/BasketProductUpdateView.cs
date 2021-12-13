using BookStore.Domain.Entities.Products;
using System.ComponentModel.DataAnnotations;

namespace BookStore.WebApi.Areas.Store.ViewModels.Basket;
public record BasketProductUpdateView
{
    [Required]
    public int? Id { init;  get; }

    [Required]
    [Range(Product.MinQuantity, Product.MaxQuantity)]
    public int Quantity { init; get; }

    public void Deconstruct(out int id, out int quantity) {
        id = Id.Value;
        quantity = Quantity;
    }
}

