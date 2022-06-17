namespace BookStore.TelegramBot.UseCases.Common;
internal static class CommandNames
{
    private const string SelectionCommandGroup = "selection_";

    public const string Start = "start";

    public const string Help = "help";

    public const string Novelties = SelectionCommandGroup + "novelties";

    public const string GoneOnSale = SelectionCommandGroup + "gone_on_sale";

    public const string BackOnSale = SelectionCommandGroup + "back_on_sale";

    public const string CurrentDayAuthor = SelectionCommandGroup + "current_day_author";

    public const string Popular = SelectionCommandGroup + "popular";

    public static string[] All => new string[]
    {
        Start, Help, Novelties, GoneOnSale, BackOnSale, CurrentDayAuthor, Popular
    };

}
