using BookStore.Application.Dto;

namespace BookStore.Application.Queries.Battle.GetBattleInfo;

public record BattleBookPreviewDto
{
    public int BookId { init; get; }
    public int BattleBookId { init; get; }
    public string Name { init; get; }
    public int Cost { init; get; }
    public ImageDto TitleImage { init; get; }
    public string AuthorName { init; get; }
    public int TotalVotingPointCount { init; get; }
}

