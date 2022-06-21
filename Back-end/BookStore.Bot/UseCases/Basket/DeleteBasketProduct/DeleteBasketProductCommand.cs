using AutoMapper;
using BookStore.Application.Commands.Basket.ChangeBasketProductQuantity;
using BookStore.Bot.Exceptions;
using BookStore.Bot.Infrastructure;
using BookStore.Bot.Providers;
using BookStore.Bot.UseCases.Basket.ChangeBasketProductQuantity;
using BookStore.Bot.UseCases.Common;
using Microsoft.Extensions.Options;
using RestSharp;
using System.Net;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BookStore.Bot.UseCases.Basket.DeleteBasketProduct;

internal record DeleteBasketProductCommand(Update Update) : TelegramBotCommand(Update);

internal class DeleteBasketProductCommandHandler : TelegramBotCommandHandler<DeleteBasketProductCommand>
{
    private AuthorizedRestClient RestClient { get; }
    private ITelegramBotClient BotClient { get; }
    private BackEndSettings Settings { get; }
    public DeleteBasketProductCommandHandler(
        AuthorizedRestClient restClient,
        IOptions<BackEndSettings> settingsOption,
        ITelegramBotClient botClient)
    {
        RestClient = restClient;
        BotClient = botClient;
        Settings = settingsOption.Value;
    }


    protected override async Task Handle(DeleteBasketProductCommand request, CancellationToken cancellationToken)
    {
        var provider = new DeleteBasketProductCommandProvider(request.Update);

        var chatId = provider.GetChatId();
        var basketProductId = provider.GetBasketProductIdToDelete();

        var deletionPath = $"{Settings.BasketPath}{basketProductId}";

        try
        {
            await RestClient.SendRequestAsync(
                telegramId: chatId,
                requestFunction: (client, cancellationToken) => client.DeleteAsync(new RestRequest(deletionPath), cancellationToken),
                cancellationToken);
        }
        catch (Exception exception)
        {
            string errorMessage = exception switch
            {
                UnauthorizedException => exception.Message,
                HttpRequestException httpException
                    when httpException.StatusCode == HttpStatusCode.NotFound => "Комикс уже удалён из корзины."
            };

            await BotClient.SendTextMessageAsync(chatId, errorMessage, cancellationToken: cancellationToken);

            return;
        }

        await BotClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Удаление прошло успешно.", 
            cancellationToken: cancellationToken);
    }
}

