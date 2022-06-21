using BookStore.Bot.Exceptions;
using BookStore.Bot.Extensions;
using BookStore.Bot.Infrastructure;
using BookStore.Bot.Providers;
using BookStore.Bot.UseCases.Common;
using Microsoft.Extensions.Options;
using RestSharp;
using System.Net;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BookStore.Bot.UseCases.Basket.CleanBasket;

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
        catch (Exception exception)
        {
            string errorMessage = exception switch
            {
                UnauthorizedException => exception.Message,
                HttpRequestException httpException
                    when httpException.StatusCode == HttpStatusCode.BadRequest => "Корзина пуста."
            };

            await BotClient.SendTextMessageAsync(chatId, errorMessage, cancellationToken: cancellationToken);

            return;
        }

        await BotClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Очистка корзины прошла успешно.",
            cancellationToken: cancellationToken);
    }
}

