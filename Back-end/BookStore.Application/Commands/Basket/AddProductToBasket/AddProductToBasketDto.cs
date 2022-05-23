using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Commands.Basket.AddProductToBasket;
public record AddProductToBasketDto
{
    [Required]
    public int? ProductId { init; get; }
}
