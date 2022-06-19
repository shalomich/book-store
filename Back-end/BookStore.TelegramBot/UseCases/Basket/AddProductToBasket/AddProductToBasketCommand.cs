using BookStore.Application.Commands.Basket.AddProductToBasket;
using BookStore.TelegramBot.Providers;
using BookStore.TelegramBot.UseCases.Common;
using Microsoft.Extensions.Options;
using RestSharp;
using System.Net;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BookStore.TelegramBot.UseCases.Basket.AddProductToBasket;

internal record AddProductToBasketCommand(Update Update) : TelegramBotCommand(Update);

internal class AddProductToBasketCommandHandler : TelegramBotCommandHandler<AddProductToBasketCommand>
{
    private AuthorizedRestClient RestClient { get; }
    private ITelegramBotClient BotClient { get; }
    private BackEndSettings Settings { get; }
    public AddProductToBasketCommandHandler(
        AuthorizedRestClient restClient,
        IOptions<BackEndSettings> settingsOption,
        ITelegramBotClient botClient)
    {
        RestClient = restClient;
        BotClient = botClient;
        Settings = settingsOption.Value;
    }


    protected override async Task Handle(AddProductToBasketCommand request, CancellationToken cancellationToken)
    {
        var provider = new AddProductToBasketCommandProvider(request.Update);

        var chatId = provider.GetChatId();

        var addToBasketDto = new AddProductToBasketDto
        {
            ProductId = provider.GetProductId()
        };

        try
        {
            await RestClient.SendRequestAsync(
                telegramId: chatId,
                requestFunction: (client, cancellationToken) => client.PostAsync(new RestRequest(Settings.BasketPath)
                    .AddBody(addToBasketDto), cancellationToken),
                cancellationToken: cancellationToken);
        }
        catch (InvalidOperationException exception)
        {
            await BotClient.SendTextMessageAsync(chatId, exception.Message, cancellationToken: cancellationToken);

            return;
        }
        catch (HttpRequestException exception)
        {
            if (exception.StatusCode == HttpStatusCode.BadRequest)
            {
                await BotClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Комикс уже добавлен в корзину.",
                    cancellationToken: cancellationToken);
            }

            return;
        }

        await BotClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Добавление в корзину прошло успешно",
            cancellationToken: cancellationToken);
    }
}

