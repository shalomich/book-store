namespace BookStore.TelegramBot.UseCases.Common;
internal static class CommandNames
{
    public const string Start = "start";

    public const string Help = "help";

    public const string Authenticate = "auth";

    public const string SelectionGroup = "selection_";

    public const string Novelties = SelectionGroup + "novelties";

    public const string GoneOnSale = SelectionGroup + "gone_on_sale";

    public const string BackOnSale = SelectionGroup + "back_on_sale";

    public const string CurrentDayAuthor = SelectionGroup + "current_day_author";

    public const string Popular = SelectionGroup + "popular";

    public const string BattleGroup = "battle_";

    public const string ShowBattle = BattleGroup + "show";

    public const string CastVote = BattleGroup + "cast_vote";

    public const string SpendVotingPoints = BattleGroup + "spend_points";
    public static string[] All => new string[]
    {
        Start, Help, Authenticate,
        Novelties, GoneOnSale, BackOnSale, CurrentDayAuthor, Popular,
        ShowBattle, CastVote, SpendVotingPoints
    };

    public static string[] NotForUser => new string[]
    {
        Start, CastVote
    };
}
