using BookStore.TelegramBot.Extensions;
using Telegram.Bot.Types;

namespace BookStore.TelegramBot.UseCases.Basket.DeleteBasketProduct;
internal class DeleteBasketProductCommandProvider
{
    public Update Update { get; }
    public DeleteBasketProductCommandProvider(
        Update update)
    {
        Update = update;
    }

    public long GetChatId()
    {
        return Update.CallbackQuery.Message.Chat.Id;
    }

    public int GetBasketProductIdToDelete()
    {
        var commandArgs = Update.TryGetCommand().CommandArgs;

        return int.Parse(commandArgs[0]);
    }
}

