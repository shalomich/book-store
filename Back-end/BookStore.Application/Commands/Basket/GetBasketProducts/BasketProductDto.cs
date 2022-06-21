using BookStore.Application.Dto;

namespace BookStore.Application.Commands.Basket.GetBasketProducts;
public record BasketProductDto
{
    public int Id { init; get; }
    public string Name { init; get; }
    public int Cost { init; get; }
    public int Quantity { init; get; }
    public int ProductQuantity { init; get; }
    public int ProductId { init; get; }
    public ImageDto TitleImage { init; get; }
}

