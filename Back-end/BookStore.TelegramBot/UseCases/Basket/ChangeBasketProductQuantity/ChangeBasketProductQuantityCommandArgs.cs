namespace BookStore.TelegramBot.UseCases.Basket.ChangeBasketProductQuantity;
internal record ChangeBasketProductQuantityCommandArgs
{
    public int BasketProductId { get; init; }
    public int MaxQuantity { get; init; }
    public int CurrentQuantity { get; init; }
    public int ChosenQuantity { get; init; }
}

