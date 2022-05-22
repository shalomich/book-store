namespace BookStore.Application.Commands.Catalog.GetBookCard;
public record BookCardTagDto
{
    public string Name { get; init; }
    public string GroupName { get; init; }
    public string ColorHex { get; init; }
}

