namespace BookStore.TelegramBot.UseCases.ViewSelection;
public record TelegramPreviewDto
{
    public string Name { init; get; }
    public string AuthorName { init; get; }
    public string PublisherName { init; get; }
    public int Cost { init; get; }
    public double? DiscountCost { init; get; }
    public string FileUrl { init; get; }
    public string StoreUrl { init; get; }
}