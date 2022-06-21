
using BookStore.Bot.Extensions;
using Telegram.Bot.Types;

namespace BookStore.Bot.UseCases.Basket.AddProductToBasket;
internal class AddProductToBasketCommandProvider
{
    private Update Update { get; }

    public AddProductToBasketCommandProvider(
        Update update)
    {
        Update = update;
    }

    public long GetChatId()
    {
        return Update.CallbackQuery.Message.Chat.Id;
    }

    public int GetProductId()
    {
        var commandArgs = Update.TryGetCommand().CommandArgs;

        return int.Parse(commandArgs[0]);
    }
}

