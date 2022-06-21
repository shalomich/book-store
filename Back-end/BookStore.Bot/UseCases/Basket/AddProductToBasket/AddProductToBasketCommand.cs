using BookStore.Application.Commands.Basket.AddProductToBasket;
using BookStore.Bot.Exceptions;
using BookStore.Bot.Infrastructure;
using BookStore.Bot.Providers;
using BookStore.Bot.UseCases.Common;
using Microsoft.Extensions.Options;
using RestSharp;
using System.Net;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BookStore.Bot.UseCases.Basket.AddProductToBasket;

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
        catch (Exception exception)
        {
            string errorMessage = exception switch
            {
                UnauthorizedException => exception.Message,
                HttpRequestException httpException
                    when httpException.StatusCode == HttpStatusCode.BadRequest => "Комикс уже добавлен в корзину."
            };

            await BotClient.SendTextMessageAsync(chatId, errorMessage, cancellationToken: cancellationToken);

            return;
        }
        
        await BotClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Добавление в корзину прошло успешно.",
            cancellationToken: cancellationToken);
    }
}

