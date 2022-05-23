
using BookStore.Application.Dto;

namespace BookStore.Application.Commands.Selection.Common;
public record PreviewDto
{
    public int Id { init; get; }
    public string Name { init; get; }
    public int Cost { init; get; }
    public int? DiscountPercentage { init; get; }
    public ImageDto TitleImage { init; get; }
    public string AuthorName { init; get; }
    public string PublisherName { init; get; }
}

