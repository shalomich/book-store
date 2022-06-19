using BookStore.TelegramBot.Extensions;
using BookStore.TelegramBot.Providers;
using BookStore.TelegramBot.UseCases.Common;
using Microsoft.Extensions.Options;
using RestSharp;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BookStore.TelegramBot.UseCases.Basket.CleanBasket;

internal record CleanBasketCommand(Update Update) : TelegramBotCommand(Update);

internal class CleanBasketCommandHandler : TelegramBotCommandHandler<CleanBasketCommand>
{
    private AuthorizedRestClient RestClient { get; }
    private ITelegramBotClient BotClient { get; }
    private BackEndSettings Settings { get; }
    public CleanBasketCommandHandler(
        AuthorizedRestClient restClient,
        IOptions<BackEndSettings> settingsOption,
        ITelegramBotClient botClient)
    {
        RestClient = restClient;
        BotClient = botClient;
        Settings = settingsOption.Value;
    }


    protected override async Task Handle(CleanBasketCommand request, CancellationToken cancellationToken)
    {
        var chatId = request.Update.GetChatId();
     
        try
        {
            await RestClient.SendRequestAsync(
                telegramId: chatId,
                requestFunction: (client, cancellationToken) => client.DeleteAsync(new RestRequest(Settings.BasketPath), cancellationToken),
                cancellationToken);
        }
        catch (InvalidOperationException exception)
        {
            await BotClient.SendTextMessageAsync(chatId, exception.Message, cancellationToken: cancellationToken);

            return;
        }
        catch (Exception)
        {
            await BotClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Корзина пуста.",
                cancellationToken: cancellationToken);

            return;
        }

        await BotClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Очистка корзины прошла успешно.",
            cancellationToken: cancellationToken);
    }
}

