namespace BookStore.TelegramBot.UseCases.Basket.ViewBasketProducts;
internal record BasketProductViewModel
{
    public int Id { init; get; }
    public string Name { init; get; }
    public int Cost { init; get; }
    public int Quantity { init; get; }
    public int MaxQuantity { init; get; }
    public string FileUrl { init; get; }
    public string StoreUrl { init; get; }
}

