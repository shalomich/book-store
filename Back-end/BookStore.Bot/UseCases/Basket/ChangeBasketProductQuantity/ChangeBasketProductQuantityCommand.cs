using AutoMapper;
using BookStore.Application.Commands.Basket.ChangeBasketProductQuantity;
using BookStore.Bot.Infrastructure;
using BookStore.Bot.Providers;
using BookStore.Bot.UseCases.Common;
using Microsoft.Extensions.Options;
using RestSharp;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BookStore.Bot.UseCases.Basket.ChangeBasketProductQuantity;

internal record ChangeBasketProductQuantityCommand(Update Update) : TelegramBotCommand(Update);

internal class ChangeBasketProductQuantityCommandHandler : TelegramBotCommandHandler<ChangeBasketProductQuantityCommand>
{
    private AuthorizedRestClient RestClient { get; }
    private ITelegramBotClient BotClient { get; }
    private CallbackCommandRepository CallbackCommandRepository { get; }
    private BackEndSettings Settings { get; }
    public ChangeBasketProductQuantityCommandHandler(
        AuthorizedRestClient restClient,
        IOptions<BackEndSettings> settingsOption,
        ITelegramBotClient botClient,
        CallbackCommandRepository callbackCommandRepository)
    {
        RestClient = restClient;
        BotClient = botClient;
        CallbackCommandRepository = callbackCommandRepository;
        Settings = settingsOption.Value;
    }


    protected override async Task Handle(ChangeBasketProductQuantityCommand request, CancellationToken cancellationToken)
    {
        var provider = new ChangeBasketProductQuantityCommandProvider(request.Update);

        bool isCallback = await CallbackCommandRepository.HasAsync(CommandNames.ChangeBasketProductQuantity,
            provider.GetChatId(), cancellationToken);

        if (!isCallback)
        {
            await ChooseQuantityAsync(provider, cancellationToken);
        }
        else
        {
            var (changeQuantityDto, success) = await TryGetChosenQuantity(provider, cancellationToken);

            if (!success)
            {
                return;
            }

            await ChangeBasketProductQuantity(changeQuantityDto, provider, cancellationToken);
        }
    }

    private async Task ChooseQuantityAsync(ChangeBasketProductQuantityCommandProvider provider, 
        CancellationToken cancellationToken)
    {
        var chatId = provider.GetChatId();
        var commandsArgs = provider.GetCommandArgs();

        await BotClient.SendTextMessageAsync(
            chatId: chatId,
            text: $"Выберите необходмиое количество (" +
                $"текущее - {commandsArgs.CurrentQuantity}, " +
                $"максимальное - {commandsArgs.MaxQuantity}).",
            cancellationToken: cancellationToken);

        await CallbackCommandRepository.UpdateAsync(provider.GetCommandLine(), telegramId: chatId, cancellationToken);
    }

    private async Task<(ChangeBasketProductQuantityDto ChangeQuantityDto, bool Success)> TryGetChosenQuantity(ChangeBasketProductQuantityCommandProvider provider,
        CancellationToken cancellationToken)
    {
        int chosenQuantity;
        ChangeBasketProductQuantityDto changeQuantityDto = null;

        var chatId = provider.GetChatId();
        
        try
        {
            chosenQuantity = provider.GetChoosenQuantity();
        }
        catch (Exception)
        {
            await BotClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Некорретное значение. Введите снова.",
                cancellationToken: cancellationToken);

            return (changeQuantityDto, false);
        }

        var commandLine = await CallbackCommandRepository.GetAsync(chatId, cancellationToken);

        var commandArgs = provider.GetCommandArgs(commandLine);

        if (chosenQuantity > commandArgs.MaxQuantity)
        {
            await BotClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"Количество товара не может превышать {commandArgs.MaxQuantity}.",
                cancellationToken: cancellationToken);

            return (changeQuantityDto, false);
        }

        changeQuantityDto = new ChangeBasketProductQuantityDto
        {
            Id = commandArgs.BasketProductId,
            Quantity = chosenQuantity
        };

        return (changeQuantityDto, true);
    }

    private async Task ChangeBasketProductQuantity(ChangeBasketProductQuantityDto changeQuantityDto,
        ChangeBasketProductQuantityCommandProvider provider, CancellationToken cancellationToken)
    {
        var chatId = provider.GetChatId();

        await RestClient.SendRequestAsync(
            telegramId: chatId,
            requestFunction: (client, cancellationToken) => client.PutAsync(new RestRequest(Settings.BasketPath)
                .AddBody(changeQuantityDto), cancellationToken),
            cancellationToken: cancellationToken);

        await CallbackCommandRepository.RemoveAsync(CommandNames.ChangeBasketProductQuantity, chatId, cancellationToken);

        await BotClient.SendTextMessageAsync(
            chatId: chatId,
            text: $"Изменение количества товара прошло успешно.",
            cancellationToken: cancellationToken);
    }
}

