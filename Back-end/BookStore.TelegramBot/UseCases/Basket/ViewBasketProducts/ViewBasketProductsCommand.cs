using AutoMapper;
using BookStore.Application.Commands.Basket.GetBasketProducts;
using BookStore.TelegramBot.Exceptions;
using BookStore.TelegramBot.Providers;
using BookStore.TelegramBot.UseCases.Common;
using Microsoft.Extensions.Options;
using RestSharp;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BookStore.TelegramBot.UseCases.Basket.ViewBasketProducts;

internal record ViewBasketProductsCommand(Update Update) : TelegramBotCommand(Update);

internal class ViewBasketProductsCommandHandler : TelegramBotCommandHandler<ViewBasketProductsCommand>
{
    private AuthorizedRestClient RestClient { get; }
    private ITelegramBotClient BotClient { get; }
    private IMapper Mapper { get; }
    private BackEndSettings Settings { get; }
    public ViewBasketProductsCommandHandler(
        AuthorizedRestClient restClient,
        IOptions<BackEndSettings> settingsOption,
        ITelegramBotClient botClient,
        IMapper mapper)
    {
        RestClient = restClient;
        BotClient = botClient;
        Mapper = mapper;
        Settings = settingsOption.Value;
    }


    protected override async Task Handle(ViewBasketProductsCommand request, CancellationToken cancellationToken)
    {
        var provider = new ViewBasketProductsCommandProvider(request.Update);

        var (basketProducts, successToGetBasketProducts) = await TryGetBasketProductsAsync(provider, cancellationToken);

        if (!successToGetBasketProducts)
        {
            return;
        }

        await ViewBasketProductsAsync(basketProducts, provider, cancellationToken);
    }

    private async Task<(IEnumerable<BasketProductViewModel> BasketProducts, bool SuccessToGetBasketProducts)> TryGetBasketProductsAsync(
        ViewBasketProductsCommandProvider provider,CancellationToken cancellationToken)
    {
        var chatId = provider.GetChatId();

        await BotClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Пытаемся достать товары из вашей корзины...",
            cancellationToken: cancellationToken);

        IEnumerable<BasketProductDto> basketProductDtos;
        IEnumerable<BasketProductViewModel> basketProductViewModels = null;

        try
        {
            basketProductDtos = await RestClient.SendRequestAsync<IEnumerable<BasketProductDto>>(
                telegramId: chatId,
                requestFunction: (client, cancellationToken) => client.GetAsync(new RestRequest(Settings.BasketPath),
                    cancellationToken),
                cancellationToken: cancellationToken);
        }
        catch (UnauthorizedException exception)
        {
            await BotClient.SendTextMessageAsync(chatId, exception.Message, cancellationToken: cancellationToken);

            return (basketProductViewModels, false);
        }

        if (!basketProductDtos.Any())
        {
            await BotClient.SendTextMessageAsync(
               chatId: chatId,
               text: "Корзина пуста.",
               cancellationToken: cancellationToken);

            return (basketProductViewModels, false);
        }

        basketProductViewModels = basketProductDtos
            .Select(dto => Mapper.Map<BasketProductViewModel>(dto))
            .ToList();

        return (basketProductViewModels, true);
    }

    private async Task ViewBasketProductsAsync(IEnumerable<BasketProductViewModel> basketProducts,
        ViewBasketProductsCommandProvider provider, CancellationToken cancellationToken)
    {
        var chatId = provider.GetChatId();

        foreach (var basketProduct in basketProducts)
        {
            await BotClient.SendPhotoAsync(
                chatId: chatId,
                photo: basketProduct.FileUrl,
                caption: provider.GetBasketProductHtml(basketProduct),
                parseMode: ParseMode.Html,
                cancellationToken: cancellationToken);

            await BotClient.SendTextMessageAsync(
               chatId: chatId,
               text: "Действия с товаром",
               replyMarkup: provider.BuildBasketProductActions(basketProduct),
               cancellationToken: cancellationToken);
        }

        await BotClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Также есть возможность.",
            replyMarkup: provider.BuildDeleteAllButton(),
            cancellationToken: cancellationToken);
    }
}

