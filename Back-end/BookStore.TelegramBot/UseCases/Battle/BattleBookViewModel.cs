namespace BookStore.TelegramBot.UseCases.Battle;
internal record BattleBookViewModel
{
    public string Name { init; get; }
    public string AuthorName { init; get; }
    public int Cost { init; get; }
    public int TotalVotingPointCount { init; get; }
    public int BattleBookId { init; get; }
    public string FileUrl { init; get; }
    public string StoreUrl { init; get; }
}

