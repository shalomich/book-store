using BookStore.TelegramBot.UseCases.Common;
using System.Text;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BookStore.TelegramBot.UseCases.Basket.ViewBasketProducts;
internal class ViewBasketProductsCommandProvider
{
    private Update Update { get; } 
    public ViewBasketProductsCommandProvider(
        Update update)
    {
        Update = update;
    }

    public long GetChatId()
    {
        return Update.Message.Chat.Id;
    }

    public string GetBasketProductHtml(BasketProductViewModel basketProduct)
    {
        var builder = new StringBuilder();

        builder.Append($"<b>Название: </b>" +
            $"<a href=\"{basketProduct.StoreUrl}\"><i>{basketProduct.Name}</i></a>\n");
        builder.Append($"<b>Цена: </b> {basketProduct.Cost}\n");
        builder.Append($"<u><b>Количество в корзине: </b> {basketProduct.Quantity}</u>\n");

        return builder.ToString();
    }

    public InlineKeyboardMarkup BuildBasketProductActions(BasketProductViewModel basketProduct)
    {
        var navigationButtons = new List<InlineKeyboardButton>()
        {
            InlineKeyboardButton.WithCallbackData(
                text: "Поменять количество",
                callbackData: CommandLineParser.ToCommandLine(
                    commandName: CommandNames.ChangeBasketProductQuantity,
                    commandArgs: new object[]
                    {
                        basketProduct.Id, basketProduct.MaxQuantity, basketProduct.Quantity
                    })),
            InlineKeyboardButton.WithCallbackData(
                text: "Удалить из корзины",
                callbackData: CommandLineParser.ToCommandLine(
                    CommandNames.DeleteChoosen, basketProduct.Id))
        };

        return new InlineKeyboardMarkup(navigationButtons);
    }

    public InlineKeyboardMarkup BuildDeleteAllButton()
    {
        var navigationButtons = new List<InlineKeyboardButton>()
        {
            InlineKeyboardButton.WithCallbackData(
                text: "Очистить корзину",
                callbackData: CommandLineParser.ToCommandLine(
                    CommandNames.DeleteAll)),
        };

        return new InlineKeyboardMarkup(navigationButtons);
    }
}

