namespace BookStore.Bot.Providers;
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

    public const string LastViewed = SelectionGroup + "last_viewed";

    public const string CanBeInteresting = SelectionGroup + "can_be_interesting";

    public const string SpecialForYou = SelectionGroup + "special_for_you";

    private const string BattleGroup = "battle_";

    public const string ShowBattle = BattleGroup + "show";

    public const string CastVote = BattleGroup + "cast_vote";

    public const string SpendVotingPoints = BattleGroup + "spend_points";

    private const string BasketGroup = "basket_";

    public const string ShowBasket = BasketGroup + "show";

    public const string AddToBasket = BasketGroup + "add";

    public const string ChangeBasketProductQuantity = BasketGroup + "change_quantity";

    public const string DeleteChoosen = BasketGroup + "delete";

    public const string DeleteAll = BasketGroup + "delete_all";




    public static string[] All => new string[]
    {
        Start, Help, Authenticate,
        Novelties, GoneOnSale, BackOnSale, CurrentDayAuthor, Popular, LastViewed, CanBeInteresting, SpecialForYou,
        ShowBattle, CastVote, SpendVotingPoints,
        ShowBasket, AddToBasket, ChangeBasketProductQuantity, DeleteChoosen, DeleteAll
    };

    public static string[] NotForUser => new string[]
    {
        Start, CastVote, AddToBasket, ChangeBasketProductQuantity, DeleteChoosen
    };
}
