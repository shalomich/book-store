using BookStore.Application.Dto;

namespace BookStore.WebApi.Areas.Store.ViewModels.Basket;

public record BasketProductView
{
    public int Id { init; get; }
    public string Name { init; get; }
    public int Cost { init; get; }
    public int Quantity { init; get; }
    public ImageDto TitleImage { init; get; }
}
