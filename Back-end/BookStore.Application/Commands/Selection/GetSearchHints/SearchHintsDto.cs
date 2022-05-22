namespace BookStore.Application.Commands.Selection.GetSearchHints;
public record SearchHintsDto
{
    public string[] Books { init; get; }
    public string[] Authors { init; get; }
    public string[] Publishers { init; get; }
}

